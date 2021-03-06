using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int VisitTime { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
