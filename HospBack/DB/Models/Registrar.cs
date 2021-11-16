using System;
using System.Collections.Generic;

#nullable disable

namespace HospBack.DB
{
    public partial class Registrar
    {
        public Registrar()
        {
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
