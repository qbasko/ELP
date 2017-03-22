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
            //var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();
            //var assemblies = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId);
            //var assemblyList = new List<Assembly>();
            //foreach (var assemblyName in assemblies)
            //{
            //    var assembly = assemblyName.GetType().GetTypeInfo().Assembly;
            //    assemblyList.Add(assembly);
            //}
            //builder.RegisterAssemblyTypes(assemblyList.ToArray()).AsImplementedInterfaces().InstancePerRequest();


            //builder.RegisterType<MembershipService>().As<IMembershipService>();
            //builder.RegisterType<UserService>().As<IUserService>();
            //builder.RegisterType<RoleService>().As<IRoleService>();
            //builder.RegisterType<UserRoleService>().As<IUserRoleService>();
            //builder.RegisterType<EncryptionService>().As<IEncryptionService>();

            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(IDetermineServicesAssembly).GetTypeInfo().Assembly).AsImplementedInterfaces();



        }
    }
}
