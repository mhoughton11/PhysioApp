using System;

namespace DIYHIIT.Library.Contracts
{
    public interface IExercise : IBaseModel
    {
        int Index { get; set; }

        string DisplayName { get; set; }
        string ImageURL { get; }
        string Description { get; set; }
        double Duration { get; set; }



    }
}
