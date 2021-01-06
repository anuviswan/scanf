using System.Threading.Tasks;

namespace Scanf.Test.SF1008.TestData.NoDiagnostics
{
    class AsyncMethodFollowingNamingConventions
    {
        public async Task GetDataAsync()
        {
            await Task.Delay(1000);
        }

        public async Task<int> GetValueAsync()
        {
            await Task.Delay(1000);
            return await Task.FromResult(1);
        }

    }
}
