//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using static DIYHIIT.Models.Constants;

//namespace DIYHIIT.Models.Workout
//{
//    public class Workout : IWorkout
//    {
//        public int ID { get; set; }
//        public int ExerciseCount { get => Exercises.Count; }

//        public string Name { get; set; }
//        public string Type { get; set; }
//        public string ExercisesString { get; set; }
//        public string CellBackgroundColour { get => GetBackgroundColour(Type); }
//        public string Focus { get => GetBodyFocus(Exercises); }

//        public double RestInterval { get; set; }
//        public double ActiveInterval { get; set; }
//        public double Effort { get; set; }
//        public double Duration { get => GetDuration(); }

//        public byte[] BodyFocus { get; set; }

//        public DateTime DateAdded { get; set; }
//        public DateTime DateUsed { get; set; }
        

//        public List<Exercise.Exercise> Exercises { get; set; }
        

//        public Workout()
//        {
//            Exercises = new List<Exercise.Exercise>();
//        }

//        /// <summary>
//        /// Print out a summary of the workout instantiation.
//        /// </summary>
//        public void DebugObject()
//        {
//            Debug.WriteLine(" *** ");
//            try
//            {
//                Debug.WriteLine($"Debugging workout: {Name}");
//                Debug.WriteLine($"ID: {ID}");
//                Debug.WriteLine($"Body focus: {BodyFocus}");
//                Debug.WriteLine(" --- ");
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Debugging core details failed.");
//                Debug.WriteLine(ex.Message);

//                return;
//            }
            
//            try
//            {
//                Debug.WriteLine($"Rest Interval workout: {RestInterval}");
//                Debug.WriteLine($"Active Interval workout: {ActiveInterval}");
//                Debug.WriteLine($"Exercise string: {ExercisesString}");
//                Debug.WriteLine($"Workout contains {Exercises.Count} exercises:");

//                if (Exercises.Count > 0)
//                {
//                    foreach (var exercise in Exercises)
//                        exercise.DebugObject();
//                }

//                if (DateAdded != null)
//                    Debug.WriteLine($"Date created: {DateAdded.Date.ToShortDateString()}");

//                if (DateUsed != null)
//                    Debug.WriteLine($"Date last used: {DateUsed.Date.ToShortDateString()}");
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Debugging detailed values failed.");
//                Debug.WriteLine(ex.ToString());
//            }

//            Debug.WriteLine(" *** ");
//        }

//        /// <summary>
//        /// Get the background colour for the display depending on the type of workout.
//        /// </summary>
//        /// <param name="workoutType">The type of workout.</param>
//        /// <returns>The background colour for the list cell as a hex string.</returns>
//        public string GetBackgroundColour(string workoutType)
//        {
//            string background;
//            switch (Type)
//            {
//                case "HIIT":
//                    background = HIITBackground;
//                    break;

//                case "Calisthenics":
//                    background = CalisthenicsBackground;
//                    break;

//                case "Yoga":
//                    background = YogaBackground;
//                    break;

//                case "Pilates":
//                    background = PilatesBackground;
//                    break;

//                default:
//                    background = "#FFFFFF";
//                    break;
//            }

//            return background;
//        }

//        /// <summary>
//        /// Get the duration of the workout in minutes according to the specific active/rest times and the number of moves.
//        /// </summary>
//        /// <returns>The duration of the workout.</returns>
//        public double GetDuration()
//        {
//            if (Exercises.Count == 0)
//                return 0;

//            double seconds = (ActiveInterval * Exercises.Count) + (RestInterval * Exercises.Count - 1);

//            TimeSpan t = TimeSpan.FromSeconds(seconds);
//            return t.Minutes;
//        }

//        /// <summary>
//        /// Retrieve the exercises from the app database and add them to the object exercise list.
//        /// </summary>
//        /// <param name="value">String encoded with the exercises to retrieve from the DB.</param>
//        /// <returns>The number of exercises added to the workout object.</returns>
//        public async Task GetExercises(string value)
//        {
//            // Split workout string
//            string[] exercises = value.Split(',');
//            //Debug.WriteLine($"Workout contains {exercises.Length - 1} exercises.");

//            foreach (var exercise in exercises)
//            {
//                if (!string.IsNullOrEmpty(exercise) || !string.IsNullOrWhiteSpace(exercise))
//                {
//                    var ex = await App.ExerciseDatabase.GetItemAsync(exercise);
//                    ex.Init();
//                    Exercises.Add(ex);
//                }
//            }
//        }

//        /// <summary>
//        /// Get the focus of the workout on the body.
//        /// </summary>
//        /// <param name="exercises">Exercises containing focus areas.</param>
//        /// <returns>Name of the area of focus.</returns>
//        private string GetBodyFocus(List<Exercise.Exercise> exercises)
//        {
//            string focus = "";

//            var areas = new byte[4];

//            /*
//            Workout focus codes:
//            [  0  |  1  |  2  |  3  ]
//            [upper|lower|core |full
//            */

//            try
//            {
//                foreach (var ex in exercises)
//                {
//                    for (int i = 0; i < 4; i++)
//                    {
//                        areas[i] += ex.BodyFocus[i];
//                    }
//                }

//                byte maxValue = areas.Max();
//                int maxIndex = areas.ToList().IndexOf(maxValue);

//                switch (maxIndex)
//                {
//                    case 0:
//                        focus = "Upper Body";
//                        break;

//                    case 1:
//                        focus = "Lower Body";
//                        break;

//                    case 2:
//                        focus = "Core";
//                        break;

//                    case 3:
//                        focus = "Full Body";
//                        break;

//                    default:
//                        focus = "Default";
//                        throw new Exception();
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Couldn't get body focus");
//                Debug.WriteLine(ex.Message);
//            }

//            return focus;
//        }
//    }
//}
