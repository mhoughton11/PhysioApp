using DIYHIIT.Services;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

using static DIYHIIT.Models.Constants;

namespace DIYHIIT.Models.Exercise
{
    public class Exercise : IExercise
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public byte[] BodyFocus { get; set; }
        public double Duration { get; set; }
        public string BackgroundColour { get; set; }
        
        public int Index { get; set; }

        public string ImageName
        {
            get => Name + ".jpg";
        }

        public Exercise()
        {
            
        }

        public void Init()
        {
            if (BodyFocus != null)
                return;

            BodyFocus = new byte[4];
        }

        public void DebugObject()
        {
            try
            {
                Debug.WriteLine(" *** ");
                Debug.WriteLine($"Debugging Exercise: {Name}");
                Debug.WriteLine($"ID: {ID}");
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
