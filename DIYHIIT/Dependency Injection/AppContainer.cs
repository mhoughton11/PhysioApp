using System;
using System.Diagnostics;
using Autofac;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Repository;
using DIYHIIT.Services.Data;
using DIYHIIT.Services.General.Dialog;
using DIYHIIT.ViewModels;

namespace DIYHIIT.DependencyInjection
{
    public static class AppContainer
    {
        public static IContainer Container { get; set; }
    }
}
