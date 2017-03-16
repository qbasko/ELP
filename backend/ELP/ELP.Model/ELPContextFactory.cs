using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELP.Model
{
    public class ELPContextFactory : IDbContextFactory<ELPContext>
    {
        public ELPContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ELPContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ELPdb;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ELPContext(builder.Options);
        }
    }
}
