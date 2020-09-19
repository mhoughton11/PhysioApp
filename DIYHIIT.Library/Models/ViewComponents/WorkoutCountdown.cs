using System;
using Xamarin.Forms;

namespace DIYHIIT.Library.Models.ViewComponents
{
    public class WorkoutCountdown : BindableObject
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

        public void Start(int seconds = 1)
        {
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                RemainingTime = Duration - _currentTime;

                _currentTime += seconds;
                var ticked = RemainingTime > 1;

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
