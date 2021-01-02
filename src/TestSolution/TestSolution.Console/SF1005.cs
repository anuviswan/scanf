using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestSolution.Console
{
    class SF1005
    {
        public void NoAsyncMethod()
        {
            Expression.Empty();
        }

        public async void AsyncVoidMethod()
        {
            await Task.FromResult(false);
        }

        public async Task AsyncTaskMethod()
        {
            await Task.FromResult(false);
        }
    }
}
