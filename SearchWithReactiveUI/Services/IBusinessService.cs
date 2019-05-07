using System;
using System.Threading.Tasks;

namespace SearchWithReactiveUI.Services
{
    public interface IBusinessService
    {
        Task<bool> DoSomethingTakingTimeAsync();
    }
}
