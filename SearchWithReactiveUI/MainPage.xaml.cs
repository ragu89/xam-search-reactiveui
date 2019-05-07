using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SearchWithReactiveUI.Services;
using SearchWithReactiveUI.ViewModels;
using Xamarin.Forms;

namespace SearchWithReactiveUI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(RxApp.TaskpoolScheduler, new BusinessService());
        }
    }
}
