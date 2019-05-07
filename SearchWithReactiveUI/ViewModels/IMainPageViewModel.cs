using System;
using System.Collections.ObjectModel;

namespace SearchWithReactiveUI.ViewModels
{
    public interface IMainPageViewModel
    {
        ObservableCollection<string> ItemsDisplayed { get; }
        string SearchText { get; set; }
        bool IsSearchRunning { get; }

        // No ICommand, no public method exposed to launch the Search !
        // The Search is triggered observing the changes applied on the property SearchText
    }
}
