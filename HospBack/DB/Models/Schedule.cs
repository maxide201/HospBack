using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime VisitTime { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
