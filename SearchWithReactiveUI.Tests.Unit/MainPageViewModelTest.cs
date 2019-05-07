using System;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
using Moq;
using ReactiveUI.Testing;
using SearchWithReactiveUI.Services;
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
                Mock<IBusinessService> mockBusinessService = CreateMockBusinessService();

                var mainPageViewModel = new MainPageViewModel(scheduler, mockBusinessService.Object);

                mainPageViewModel.SearchText = "J";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);
                mockBusinessService.Verify(s => s.DoSomethingTakingTimeAsync(), Times.Never);

                scheduler.AdvanceByMs(500); // Change the value before the throttle is reached
                mainPageViewModel.SearchText = "Jo";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);
                mockBusinessService.Verify(s => s.DoSomethingTakingTimeAsync(), Times.Never);

                scheduler.AdvanceByMs(500); // Change the value before the throttle is reached
                mainPageViewModel.SearchText = "Joh";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);
                mockBusinessService.Verify(s => s.DoSomethingTakingTimeAsync(), Times.Never);

                scheduler.AdvanceByMs(500); // Change the value before the throttle is reached
                mainPageViewModel.SearchText = "John";
                Assert.Equal(9, mainPageViewModel.ItemsDisplayed.Count);
                mockBusinessService.Verify(s => s.DoSomethingTakingTimeAsync(), Times.Never);

                scheduler.AdvanceByMs(1000); // Now we wait until the throttle is reached
                Assert.Equal(2, mainPageViewModel.ItemsDisplayed.Count); // John & Johnatan
                mockBusinessService.Verify(s => s.DoSomethingTakingTimeAsync(), Times.Once);
            });
        }

        private static Mock<IBusinessService> CreateMockBusinessService()
        {
            var mockBusinessService = new Mock<IBusinessService>();
            mockBusinessService.Setup(s => s.DoSomethingTakingTimeAsync()).Returns(Task.FromResult(true));
            return mockBusinessService;
        }
    }
}
