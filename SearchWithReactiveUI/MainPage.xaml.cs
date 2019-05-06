using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            BindingContext = new MainPageViewModel();
        }

        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            (BindingContext as MainPageViewModel).SearchCommand.Execute(null);
        }
    }
}
