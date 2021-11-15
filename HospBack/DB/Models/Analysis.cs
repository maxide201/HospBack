using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Analysis
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime AnalyseDate { get; set; }
        public string ResultDescription { get; set; }
        public string AnalyseType { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
