using Elix.SafeAuto.Application.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Elix.SafeAuto.Application
{
    public class DriverService : IDriverService
    {
        private readonly string[] validCommands = { "Driver", "Trip" };
        private readonly string timeFormat = "HH:mm";
        private readonly CultureInfo provider = CultureInfo.InvariantCulture;
        private List<DriveCommand> commands = null;
        private List<Driver> drivers = null;
        public string Process(string contents)
        {
            string result = "";
            commands = new List<DriveCommand>();
            drivers = new List<Driver>();
            string[] lines = contents.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                var parameters = lines[i].Split(' ');
                if (parameters.Length == 0 || !validCommands.Contains(parameters[0]))
                {
                    throw new ArgumentException($"Invalid command at line {i + 1}");
                }

                switch (parameters[0])
                {
                    case "Driver":
                        if (parameters.Length != 2)
                        {
                            throw new ArgumentException($"Invalid number of fields at line {i + 1}");
                        }

                        //commands.Add(new DriveCommand() { Command = parameters[0], DriverName = parameters[1] });
                        Driver(parameters[1]);

                        break;
                    case "Trip":
                        if (parameters.Length != 5)
                        {
                            throw new ArgumentException($"Invalid number of fields at line {i + 1}");
                        }

                        DateTime startTime;
                        DateTime endTime;
                        decimal miles;
                        if (!DateTime.TryParseExact(parameters[2], timeFormat, provider, DateTimeStyles.None, out startTime))
                        {
                            throw new InvalidCastException($"Invalid start time at line {i + 1}");
                        }

                        if (!DateTime.TryParseExact(parameters[3], timeFormat, provider, DateTimeStyles.None, out endTime))
                        {
                            throw new InvalidCastException($"Invalid end time at line {i + 1}");
                        }

                        if (!decimal.TryParse(parameters[4], out miles))
                        {
                            throw new InvalidCastException($"Invalid miles at line {i + 1}");
                        }

                        commands.Add(new DriveCommand() { Command = parameters[0], DriverName = parameters[1], StartTime = startTime, EndTime = endTime, Miles = miles });

                        break;
                }
            }

            //in case we add more commands with some precedence
            commands.Sort(SortByCommand);
            foreach (var command in commands)
            {
                switch (command.Command)
                {
                    case "Trip":
                        Trip(command.DriverName, command.StartTime.Value, command.EndTime.Value, command.Miles);
                        break;
                }
            }

            drivers.Sort(SortByDriver);
            for (int i = 0; i < drivers.Count; i++)
            {
                result += $"{drivers[i].Name}: {drivers[i].MilesTraveled} miles";
                if (drivers[i].MilesTraveled > 0)
                {
                    result += $" @ {drivers[i].AvgSpeed} mph";
                }

                if (i < drivers.Count - 1)
                {
                    result += Environment.NewLine;
                }
            }

            return result;
        }

        public void Driver(string name)
        {
            if (drivers == null)
            {
                drivers = new List<Driver>();
            }

            if (!drivers.Exists(d => d.Name == name))
            {
                var driver = new Driver(name);
                drivers.Add(new Models.Driver(name));
            }
        }

        public void Trip(string driverName, DateTime startTime, DateTime endTime, decimal miles)
        {
            var driver = drivers.FirstOrDefault(d => d.Name == driverName);
            if (driver == null)
            {
                throw new KeyNotFoundException($"Driver {driverName} not registered");
            }

            if (endTime < startTime)
            {
                throw new ArgumentException("Invalid start/end times");
            }

            var travelTime = endTime - startTime;
            var travelSpeed = travelTime.TotalHours > 0 ? (int)Math.Round(miles / (decimal)travelTime.TotalHours, MidpointRounding.AwayFromZero) : 0;
            if (travelSpeed >= 5 && travelSpeed <= 100)
            {
                driver.Travel(startTime, endTime, miles);
            }
        }

        private int SortByCommand(DriveCommand command1, DriveCommand command2)
        {
            return Array.IndexOf(validCommands, command1.Command).CompareTo(Array.IndexOf(validCommands, command2.Command));
        }

        private int SortByDriver(Driver driver1, Driver driver2)
        {
            return driver2.MilesTraveled.CompareTo(driver1.MilesTraveled);
        }
    }
}