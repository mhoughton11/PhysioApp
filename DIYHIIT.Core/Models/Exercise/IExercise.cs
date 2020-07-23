using System;

namespace DIYHIIT.Models.Exercise
{
    public interface IExercise : IBaseModel
    {
        int Index { get; set; }

        string DisplayName { get; set; }
        string ImageURL { get; }
        string BackgroundColour { get; set; }
        string Description { get; set; }

        double Duration { get; set; }

    }
}
