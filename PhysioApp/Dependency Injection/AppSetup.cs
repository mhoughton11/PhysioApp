using System;
using System.Diagnostics;
using Autofac;
using Autofac.Core;
using PhysioApp.Contracts;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Contracts.Services.General;
using PhysioApp.Repository;
using PhysioApp.Services.Data;
using PhysioApp.Services.General.Dialog;
using PhysioApp.ViewModels;
using PhysioApp.ViewModels.Authentication;
using PhysioApp.ViewModels.Exercises;
using PhysioApp.ViewModels.Tabs;
using PhysioApp.ViewModels.Workouts;

namespace PhysioApp.DependencyInjection
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
