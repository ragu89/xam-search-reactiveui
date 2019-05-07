using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ReactiveUI;
using SearchWithReactiveUI.Services;

namespace SearchWithReactiveUI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IBusinessService businessService;
        private readonly ObservableCollection<string> items;

        public MainPageViewModel(IScheduler scheduler, IBusinessService businessService)
        {
            this.businessService = businessService;

            items = new ObservableCollection<string> { "John", "Tom", "Alex", "Tomas", "Alexandre", "Johnatan", "Jonas", "Joel", "James" };

            ItemsDisplayed = items; // Initial value

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(1.0), scheduler) // Only run the search when SearchText hasn't changed since 1.0 second
                //.Select(searchText => searchText.ToLower())   // Doesn't work, don't know why
                .DistinctUntilChanged()
                .Subscribe(async searchText => await SearchForAsync(searchText));
        }

        private async Task SearchForAsync(string text)
        {
            Console.WriteLine($"SearchCommand called for text : {text}");

            if (string.IsNullOrEmpty(text))
            {
                ItemsDisplayed = items;
            }
            else
            {
                var lowerSearch = text.ToLower();
                var listResult = items.Where(item => item.ToLower().Contains(lowerSearch)).ToList();

                var result = await businessService.DoSomethingTakingTimeAsync();
                if (!result)
                {
                    throw new Exception("The business service has returned an error");
                }

                ItemsDisplayed = new ObservableCollection<string>(listResult);
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
