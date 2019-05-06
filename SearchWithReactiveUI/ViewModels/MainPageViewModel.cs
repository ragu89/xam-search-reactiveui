using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Xamarin.Forms;

namespace SearchWithReactiveUI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<string> items;

        public MainPageViewModel()
        {
            items = new ObservableCollection<string> { "John", "Tom", "Alex", "Tomas", "Alexandre", "Johnatan", "Jonas", "Joel", "James" };

            ItemsDisplayed = items; // Initial value

            searchCommand = new Command(async () => await SearchCommandExecuteAsync());

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(1.0), RxApp.TaskpoolScheduler) // Only run the command when SearchText hasn't changed since 1.0 second
                .DistinctUntilChanged()
                .InvokeCommand(searchCommand);
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        private ObservableCollection<string> itemsDisplayed;
        public ObservableCollection<string> ItemsDisplayed
        {
            get => itemsDisplayed;
            set
            {
                itemsDisplayed = value;
                OnPropertyChanged(nameof(ItemsDisplayed));
            }
        }

        private ICommand searchCommand { get; set; } // The Command is now private because it's not binded anymore to the view
        private async Task SearchCommandExecuteAsync()
        {
            Console.WriteLine($"SearchCommand called for text : {SearchText}");

            if(string.IsNullOrEmpty(SearchText))
            {
                ItemsDisplayed = items;
            }
            else
            {
                var lowerSearch = SearchText.ToLower();
                var listResult = items.Where(item => item.ToLower().Contains(lowerSearch)).ToList();

                await Task.Delay(900); // Faking a time consuming query during the search

                ItemsDisplayed = new ObservableCollection<string>(listResult);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
