using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Library.Models
{
    public class Exercise : IExercise
    {
        // Primary key
        public int Id { get; set; }

        public int? Index { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string BodyFocus { get; set; }
        public string ImageURL { get; set; }

        public double? Duration { get; set; }

        [Required]
        public WorkoutType Type { get; set; }

        public void DebugObject()
        {
            try
            {
                Debug.WriteLine(" *** ");
                Debug.WriteLine($"Debugging Exercise: {Name}");
                Debug.WriteLine($"ID: {Id}");
                Debug.Write($"Body focus: ");
                for (int i = 0; i < 4; i++)
                {
                    Debug.Write(BodyFocus[i].ToString());
                    Debug.Write(" ");
                }
                Debug.WriteLine("");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Core details failed.");
                Debug.WriteLine(ex.Message);
                return;
            }

            try
            {
                Debug.WriteLine($"Type: {Type}");
                if (Duration > 0)
                    Debug.WriteLine($"Duration: {Duration}");
                Debug.WriteLine(" *** ");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Further details failed.");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
