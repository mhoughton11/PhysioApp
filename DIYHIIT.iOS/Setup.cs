using System;
using Autofac;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.iOS.Authentication;

namespace DIYHIIT.iOS
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
