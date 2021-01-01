using System.Threading.Tasks;

namespace Scanf.Test.SF1005.TestData.NoDiagnostics
{
    public class AsyncMethodsWithTaskReturnType
    {
        public async Task MethodWithNoReturnType()
        {
            await Task.FromResult(true);
        }

        public async Task<int> MethodWithIntReturn()
        {
            return await Task.FromResult(1);
        }
    }
}
