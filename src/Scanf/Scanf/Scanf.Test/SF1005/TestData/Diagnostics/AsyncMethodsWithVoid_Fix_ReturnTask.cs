using System.Threading.Tasks;

namespace Scanf.Test.SF1005.TestData.Diagnostics
{
    public class AsyncMethodsWithVoid
    {
        public async Task MethodWithAsyncVoid()
        {
            await Task.FromResult(true);
        }
    }
}
