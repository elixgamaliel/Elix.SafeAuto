using System;
using System.Collections.Generic;
using System.Text;

namespace Elix.SafeAuto.Application.Models
{
    public class Driver
    {
        private decimal milesTraveled;
        private TimeSpan timeTraveled;
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int MilesTraveled
        {
            get
            {
                return (int)Math.Round(milesTraveled);
            }
        }

        public int AvgSpeed
        {
            get
            {
                return timeTraveled.TotalHours > 0 ? (int)Math.Round(milesTraveled / (decimal)timeTraveled.TotalHours, MidpointRounding.AwayFromZero) : 0;
            }
        }

        public Driver()
        {
            Id = new Guid();
        }

        public Driver(string name) : base()
        {
            Name = name;
        }

        public void Travel(DateTime start, DateTime end, decimal miles)
        {
            timeTraveled += end - start;
            milesTraveled += miles;
        }
    }
}
