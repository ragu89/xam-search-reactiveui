using System;
using Microsoft.Reactive.Testing;
using ReactiveUI.Testing;
using SearchWithReactiveUI.ViewModels;
using Xunit;

namespace SearchWithReactiveUI.Tests.Unit
{
    public class MainPageViewModelTest
    {
        [Fact]
        public void SearchTextChangedTest()
        {
            new TestScheduler().With(scheduler =>
            {
                var mainPageViewModel = new MainPageViewModel(scheduler);

                mainPageViewModel.SearchText = "J";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);

                scheduler.AdvanceByMs(500); // Change the value before the throttle is reached
                mainPageViewModel.SearchText = "Jo";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);

                scheduler.AdvanceByMs(500); // Change the value before the throttle is reached
                mainPageViewModel.SearchText = "Joh";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);

                scheduler.AdvanceByMs(500); // Change the value before the throttle is reached
                mainPageViewModel.SearchText = "John";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);

                scheduler.AdvanceByMs(1000); // Now we wait until the throttle is reached
                Assert.Equal(2, mainPageViewModel.ItemsDisplayed.Count); // John & Johnatan
            });
        }
    }
}
