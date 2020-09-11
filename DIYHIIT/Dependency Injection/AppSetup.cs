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
    public class AppSetup
    {
        private static IContainer _container;

        public IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);
            return builder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder builder)
        {
            // ViewModels
            builder.RegisterType<AddExerciseViewModel>();
            builder.RegisterType<CreateWorkoutViewModel>();
            builder.RegisterType<ExecuteWorkoutViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<PreviewWorkoutViewModel>();
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<WorkoutListViewModel>();

            // Services - Data
            builder.RegisterType<ExerciseDataService>().As<IExerciseDataService>();
            builder.RegisterType<WorkoutDataService>().As<IWorkoutDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();

            // Services - General
            builder.RegisterType<DialogService>().As<IDialogService>();

            // General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();
        }
    }
}
