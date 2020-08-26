using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Library.Models
{
    public class Workout : IWorkout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public WorkoutType Type { get; set; }

        public string Name { get; set; }
        public string BodyFocus { get; set; }

        public double? RestInterval { get; set; }
        public double? ActiveInterval { get; set; }
        public double? Effort { get; set; }
        public double? Duration { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateUsed { get; set; }

        public List<Exercise> Exercises { get; set; }

        /// <summary>
        /// Print out a summary of the workout instantiation.
        /// </summary>
        public void DebugObject()
        {
            Debug.WriteLine(" *** ");
            try
            {
                Debug.WriteLine($"Debugging workout: {Name}");
                Debug.WriteLine($"ID: {Id}");
                Debug.WriteLine($"Body focus: {BodyFocus}");
                Debug.WriteLine(" --- ");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Debugging core details failed.");
                Debug.WriteLine(ex.Message);

                return;
            }
            
            try
            {
                Debug.WriteLine($"Rest Interval workout: {RestInterval}");
                Debug.WriteLine($"Active Interval workout: {ActiveInterval}");
                Debug.WriteLine($"Workout contains {Exercises.Count} exercises:");

                if (Exercises.Count > 0)
                {
                    foreach (var exercise in Exercises)
                        exercise.DebugObject();
                }

                if (DateAdded != null)
                    Debug.WriteLine($"Date created: {DateAdded?.Date.ToShortDateString()}");

                if (DateUsed != null)
                    Debug.WriteLine($"Date last used: {DateUsed?.Date.ToShortDateString()}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Debugging detailed values failed.");
                Debug.WriteLine(ex.ToString());
            }

            Debug.WriteLine(" *** ");
        }
    }
}
