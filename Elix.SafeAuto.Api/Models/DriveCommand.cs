using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elix.SafeAuto.Api.Models
{
    public class DriveCommand
    {
        public string Command { get; set; }

        public string DriverName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal Miles { get; set; }
    }
}
