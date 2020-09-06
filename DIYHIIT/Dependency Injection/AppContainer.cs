using System;
using System.Diagnostics;
using Autofac;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.Contracts.Services.General.Navigation;
using DIYHIIT.Repository;
using DIYHIIT.Services.Data;
using DIYHIIT.Services.General.Authentication;
using DIYHIIT.Services.General.Dialog;
using DIYHIIT.ViewModels;

namespace DIYHIIT.DependencyInjection
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependancies()
        {
            var builder = new ContainerBuilder();

            // ViewModels
            builder.RegisterType<AddExerciseViewModel>();
            builder.RegisterType<CreateWorkoutViewModel>();
            builder.RegisterType<ExecuteWorkoutViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<PreviewWorkoutViewModel>();
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<WorkoutListViewModel>();

            // Services - Data
            builder.RegisterType<ExerciseDataService>().As<IExerciseDataService>();
            builder.RegisterType<WorkoutDataService>().As<IWorkoutDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();

            // Services - General
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<DialogService>().As<IDialogService>();

            // General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }
      
        public static object Resolve(Type typeName)
        {
            Debug.Write("Resolving for type: ");
            Debug.WriteLine(typeName);
            return _container.Resolve(typeName);
        }
        
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
        
    }
}
