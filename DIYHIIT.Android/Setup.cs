using System;
using Autofac;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Droid.Authentication;

namespace DIYHIIT.Droid
{
    public class Setup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
        }
    }
}
