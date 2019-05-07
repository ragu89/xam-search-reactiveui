using System;
using System.Threading.Tasks;

namespace SearchWithReactiveUI.Services
{
    public class BusinessService : IBusinessService
    {
        public async Task<bool> DoSomethingTakingTimeAsync()
        {
            await Task.Delay(900);

            // ...

            return true;
        }
    }
}
