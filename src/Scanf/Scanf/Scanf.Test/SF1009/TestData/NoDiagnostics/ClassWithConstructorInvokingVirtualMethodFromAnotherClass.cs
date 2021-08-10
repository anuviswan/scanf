namespace Scanf.Test.SF1009.TestData.NoDiagnostics
{
    class ClassWithConstructorInvokingVirtualMethodFromAnotherClass
    {
        public ClassWithConstructorInvokingVirtualMethodFromAnotherClass()
        {
            var foo = new Foo();
            foo.Bar();
        }
    }

    public class Foo
    {
        public virtual void Bar()
        {

        }
    }
     

}
