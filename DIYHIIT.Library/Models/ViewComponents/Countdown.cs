using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace DIYHIIT.Library.Models.ViewComponents
{
    public class Countdown : BindableObject
    {
        double _remainingTime;
        double _currentTime = 0;

        public event Action Completed;
        public event Action Ticked;

        public double Duration { get; set; }

        public double RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged();
            }
        }

        public void Start(double interval)
        {
            Device.StartTimer(TimeSpan.FromSeconds(interval), () =>
            {
                RemainingTime = Duration - _currentTime;

                _currentTime += interval;
                var ticked = RemainingTime > 1;

                Debug.WriteLine($"ticked. current time: {_currentTime}");

                if (ticked)
                {
                    Ticked?.Invoke();
                }
                else
                {
                    Completed?.Invoke();
                }

                return ticked;
            });
        }
    }
}
