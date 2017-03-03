using Microsoft.EntityFrameworkCore;
using System;

namespace ELP.Model
{
    public class ELPContext : DbContext
    {
        public ELPContext()
        {

        }

        public DbSet<Event> Events { get; set; }
    }
}
