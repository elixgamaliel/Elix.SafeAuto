using System;

namespace Elix.SafeAuto.Application
{
    public interface IDriverService
    {
        void Driver(string name);
        string Process(string contents);
        void Trip(string driverName, DateTime startTime, DateTime endTime, decimal miles);
    }
}