using ELP.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ELP.Model
{
    public interface IContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<IdentityUser> Users { get; set; }
        DbSet<IdentityRole> Roles { get; set; }
        //DbSet<IdentityUserRole> UserRoles { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
