using System.Threading.Tasks;

namespace Scanf.Test.SF1005.TestData.Diagnostics
{
    public class AsyncMethodsWithVoid
    {
        public async void MethodWithAsyncVoid()
        {
            await Task.FromResult(true);
        }
    }
}
