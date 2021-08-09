namespace Scanf.Test.SF1009.TestData.NoDiagnostics
{
    class ClassWithConstructorButNoVirtualMethods
    {
        public ClassWithConstructorButNoVirtualMethods()
        {
            Foo();
            Bar();
        }
        public void Foo()
        {

        }

        public void Bar()
        {

        }
    }
}
