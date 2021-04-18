namespace Scanf.Test.SF1004.TestData.NoDiagnostics
{
    public class NoPureMethods
    {
        public void NonPureMethodWithVoidReturnType()
        {

        }

        public int NonPureMethodWithIntReturnType()
        {
            return default;
        }
    }
}
