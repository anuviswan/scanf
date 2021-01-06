using System.Threading.Tasks;

namespace Scanf.Test.SF1008.TestData.Diagnostics
{
    public class AsyncMethodWithCaseInsensitiveSuffix
    {
        public async Task GetDataAsync()
        {
            await Task.Delay(100);
        }
    }
}
