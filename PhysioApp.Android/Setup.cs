using System;
using Autofac;
using PhysioApp.Contracts.Services.General;
using PhysioApp.DependencyInjection;
using PhysioApp.Droid.Authentication;

namespace PhysioApp.Droid
{
    public class Setup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
        }
    }
}
