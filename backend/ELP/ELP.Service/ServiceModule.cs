using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyModel;
using Microsoft.DotNet.InternalAbstractions;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ELP.Service
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(IDetermineServicesAssembly).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext).GetTypeInfo().Assembly);
            builder.RegisterAssemblyTypes(typeof(Microsoft.AspNetCore.Identity.UserManager<IdentityUser>).GetTypeInfo().Assembly);
        }
    }
}
