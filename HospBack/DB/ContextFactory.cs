using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospBack.DB
{
    public static class ContextFactory
    {
        public static dbContext Get(string connectionString)
        {
            // для инкапсуляции опций контекста
            var context = new dbContext(connectionString);
            return context;
        }
    }
}
