using MvvmCross.ViewModels;
using Xamarin.Forms;
using DIYHIIT.Models.Exercise;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using MvvmCross.Binding.Extensions;

namespace DIYHIIT.ViewModels
{
    public class AddExerciseViewModel : MvxViewModel
    {
        private ObservableCollection<Exercise> _flowExercises;
        public ObservableCollection<Exercise> FlowExercises 
        {
            get => _flowExercises; 
            set
            {
                _flowExercises = value;
                RaisePropertyChanged(() => FlowExercises);
            }
        }

        private List<string> _exerciseTypes;
        public List<string> ExerciseTypes
        {
            get => _exerciseTypes;
            set
            {
                _exerciseTypes = value;
                RaisePropertyChanged(() => ExerciseTypes);
            }
        }

        private string _selectedFilter = "All";
        public string SelectedFilter
        {
            get => _selectedFilter;
            set 
            {
                _selectedFilter = value;
                RaisePropertyChanged(() => SelectedFilter);

                GetExercises(value);
            }
        }

        public AddExerciseViewModel()
        {
            FlowExercises = new ObservableCollection<Exercise>();
            ExerciseTypes = new List<string>()
            {
                "All",
                "Calisthenics",
                "HIIT",
                "Pilates",
                "Stretches",
                "Yoga"
            };
            SelectedFilter = ExerciseTypes[0];
        }

        public void OnAppearing()
        {
            GetExercises();
        }

        public void ItemTapped(Exercise item)
        {
            try
            {
                MessagingCenter.Send(this, "ExerciseAdded", item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to send item using MC.");
                Debug.WriteLine(ex.Message);
            }
        }

        private async void GetExercises(string type = null)
        {
            var items = await App.ExerciseDatabase.GetItemsAsync(type);
            var sorted = items.OrderBy(o => o.Name).ToList();

            // Remove list 
            for (int i = 0; i < sorted.Count; i++)
            {
                if (sorted[i].Name == "Rest")
                    sorted.RemoveAt(i);
            }

            FlowExercises = new ObservableCollection<Exercise>(sorted);
        }
    }
}
