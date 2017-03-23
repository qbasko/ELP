using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyModel;
using Microsoft.DotNet.InternalAbstractions;
using System.Reflection;

namespace ELP.Service
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(IDetermineServicesAssembly).GetTypeInfo().Assembly).AsImplementedInterfaces();
        }
    }
}
