using System;

namespace DIYHIIT.Models
{
    public interface IBaseModel
    {
        int ID { get; set; }

        string Name { get; set; }
        string BodyFocus { get; set; }

        WorkoutType Type { get; set; }

        void DebugObject();
    }
}
