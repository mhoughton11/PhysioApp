using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIYHIIT.Views
{
    public partial class MainPage : TabbedPage
    {
        private bool _isTabPageVisible;

        public static readonly BindableProperty SelectedTabIndexProperty
            = BindableProperty.Create(nameof(SelectedTabIndex),
                                      typeof(int),
                                      typeof(MainPage), 0,
                                      BindingMode.TwoWay, null,
                                      propertyChanged: OnSelectedTabIndexChanged);

        public MainPage()
        {
            InitializeComponent();
        }

        public int SelectedTabIndex
        {
            get { return (int)GetValue(SelectedTabIndexProperty); }
            set { SetValue(SelectedTabIndexProperty, value); }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _isTabPageVisible = true;

            this.CurrentPage = this.Children[SelectedTabIndex];
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _isTabPageVisible = false;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            SelectedTabIndex = this.Children.IndexOf(this.CurrentPage);
        }

        static void OnSelectedTabIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (((MainPage)bindable)._isTabPageVisible)
            {
                // update the Selected Child-Tab page 
                // only if Tabbed Page is visible..
                ((MainPage)bindable).CurrentPage = ((MainPage)bindable).Children[(int)newValue];
            }
        }
    }
}