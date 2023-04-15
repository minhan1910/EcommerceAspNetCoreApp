using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {

        }
    }
}
