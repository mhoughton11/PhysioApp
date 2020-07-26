using System;
using Xamarin.Forms;

namespace DIYHIIT.Models
{
    public interface IBaseModel
    {
        int ID { get; set; }
        string Name { get; set; }
        byte[] BodyFocus { get; set; }
        string Type { get; set; }
        void DebugObject();
    }
}
