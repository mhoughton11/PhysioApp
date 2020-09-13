using System;
using System.ComponentModel;
using Autofac;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.DependencyInjection;
using DIYHIIT.Library.Models;
using Plugin.AutoLogin;
using Plugin.AutoLogin.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIYHIIT.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}