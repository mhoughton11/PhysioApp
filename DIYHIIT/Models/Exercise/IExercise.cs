using System;
using Xamarin.Forms;

namespace DIYHIIT.Models.Exercise
{
    public interface IExercise : IBaseModel
    {
        string DisplayName { get; set; }
        double Duration { get; set; }
        string BackgroundColour { get; set; }
        int Index { get; set; }
        string ImageName { get; }
    }
}
