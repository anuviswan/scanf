using System;
using System.Threading.Tasks;

namespace Scanf.Test.SF1005.TestData.NoDiagnostics
{
    public class AsyncVoidForEventHandlers
    {
        private Foo _fooInstance;
        public AsyncVoidForEventHandlers()
        {
            _fooInstance = new Foo();
            _fooInstance.MockEventHandler += MockHandler;
        }

        private async void MockHandler(object sender, EventArgs e)
        {
            await Task.FromResult(true);
        }
    }

    public class Foo
    {
        public event EventHandler MockEventHandler;
    }
}
