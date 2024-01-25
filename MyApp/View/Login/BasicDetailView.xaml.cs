
using Microsoft.Maui.Controls;
using MyApp.Service;
using MyApp.IService;
using MyApp.ViewModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.View.Login
{
    // BasicDetailView.xaml.cs
    public partial class BasicDetailView : ContentPage
    {

        public BasicDetailView(SignUpViewModel viewModel)
        {
            InitializeComponent();



            BindingContext = viewModel;
            viewModel.LoadStates();
        }


    }

}
