using System;
using System.Diagnostics;
using Autofac;
using PhysioApp.Contracts;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Repository;
using PhysioApp.Services.Data;
using PhysioApp.Services.General.Dialog;
using PhysioApp.ViewModels;

namespace PhysioApp.DependencyInjection
{
    public static class AppContainer
    {
        public static IContainer Container { get; set; }
    }
}
