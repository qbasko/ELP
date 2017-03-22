using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Reflection;

namespace ELP.Model
{
    public class ModelModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ELPContext>().As<IContext>();
            builder.RegisterAssemblyTypes(typeof(IDetermineModelAssembly).GetTypeInfo().Assembly).AsImplementedInterfaces();
        }
    }
}
