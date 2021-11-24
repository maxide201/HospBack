using System;
using System.Collections.Generic;

namespace HospBack.DB
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int VisitTime { get; set; }
    }
}
