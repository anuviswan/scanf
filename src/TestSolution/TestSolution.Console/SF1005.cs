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

        public SF1005()
        {
            var foo = new Foo();
            foo.SampleEvent += Foo_SampleEvent;
        }

        private async void Foo_SampleEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class Foo
    {
        public event EventHandler SampleEvent;
    }
}
