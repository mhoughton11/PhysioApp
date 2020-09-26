using System;
using System.Diagnostics;
using Autofac;
using Autofac.Core;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Repository;
using DIYHIIT.Services.Data;
using DIYHIIT.Services.General.Dialog;
using DIYHIIT.ViewModels;
using DIYHIIT.ViewModels.Authentication;
using DIYHIIT.ViewModels.Exercises;
using DIYHIIT.ViewModels.Tabs;
using DIYHIIT.ViewModels.Workouts;

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
            builder.RegisterType<RegistrationViewModel>();

            // Services - Data
            builder.RegisterType<ExerciseDataService>().As<IExerciseDataService>();
            builder.RegisterType<WorkoutDataService>().As<IWorkoutDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<FeedItemService>().As<IFeedItemService>();
            builder.RegisterType<AuditTrailDataService>().As<IAuditTrailDataService>();

            // Services - General
            builder.RegisterType<DialogService>().As<IDialogService>();

            // General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();
        }
    }
}
