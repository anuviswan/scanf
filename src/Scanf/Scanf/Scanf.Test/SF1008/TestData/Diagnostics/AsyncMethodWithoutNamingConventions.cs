using System.Threading.Tasks;

namespace Scanf.Test.SF1008.TestData.Diagnostics
{
    public class AsyncMethodWithoutNamingConventions
    {
        public async Task GetData()
        {
            await Task.Delay(100);
        }
    }
}
