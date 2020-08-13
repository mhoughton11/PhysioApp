using System;
using Xamarin.Forms;
using DIYHIIT.Views;
using DIYHIIT.Data;
using System.IO;
using DLToolkit.Forms.Controls;
using DIYHIIT.Data.Database;
using DIYHIIT.Models.Workout;
using DIYHIIT.Models.Exercise;
using System.Threading.Tasks;
using DIYHIIT.Services;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;
using DIYHIIT.Utility;
using DIYHIIT.Contracts.Services.General;

namespace DIYHIIT
{
    public partial class App : Application
    {

        static BaseDatabase<Workout> workoutDatabase;
        public static BaseDatabase<Workout> WorkoutDatabase
        {
            get
            {
                if (workoutDatabase == null)
                {
                    workoutDatabase = new BaseDatabase<Workout>(Path.Combine(FolderPath, "Workouts.db3"), "Workouts");
                }
                return workoutDatabase;
            }
        }

        
        static BaseDatabase<Workout> recentWorkouts;
        public static BaseDatabase<Workout> RecentWorkouts
        {
            get
            {
                if (recentWorkouts == null)
                {
                    recentWorkouts = new BaseDatabase<Workout>(Path.Combine(FolderPath, "Recents.db3"), "RecentWorkouts");
                }
                return recentWorkouts;
            }
        }
        

        static BaseDatabase<Exercise> exerciseDatabase;
        public static BaseDatabase<Exercise> ExerciseDatabase
        {
            get
            {
                if (exerciseDatabase == null)
                {
                    exerciseDatabase = new BaseDatabase<Exercise>(Path.Combine(FolderPath, "Exercises.db3"), "Exercises");
                }
                return exerciseDatabase;
            }
        }


        static public string FolderPath { get; private set; }

        public App()
        {
            InitializeComponent();
            FlowListView.Init();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));            

            InitializeApp();

            InitalizeNavigation();

            MainPage = new MainPage();
        }

        private async void InitalizeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        private void InitializeApp()
        {
            

            AppContainer.RegisterDependancies();
        }

        protected override void OnStart()
        {
 
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }

        private static List<Exercise> GetJsonData(string jsonFile)
        {
            List<Exercise> items = new List<Exercise>();

            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream($"DIYHIIT.Data.Json.{jsonFile}");

                using (var reader = new StreamReader(stream))
                {
                    var jsonString = reader.ReadToEnd();

                    // Convert JSON Array objects into generic list
                    items = JsonConvert.DeserializeObject<List<Exercise>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to extract data from json file: {jsonFile}");
                Debug.WriteLine(ex.Message);
            }

            return items;
        }
    }
}
