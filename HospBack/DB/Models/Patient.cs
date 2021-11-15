using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.DB
{
	public partial class Patient
	{
        public Patient()
        {
            Visits = new HashSet<Visit>();
            Analyses = new HashSet<Analysis>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Analysis> Analyses { get; set; }
    }
}
