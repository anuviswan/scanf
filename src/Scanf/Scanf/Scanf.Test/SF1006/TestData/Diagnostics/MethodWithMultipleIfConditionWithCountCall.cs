using System.Linq;

namespace Scanf.Test
{
    public class MethodWithMultipleIfConditionWithCountCall
    {
        public void Foo()
        {
            var collection1 = Enumerable.Range(1, 100);
            var collection2 = Enumerable.Range(1, 100);

            if (collection1.Count() > 0 && collection2.Count() == 0)
            {

            }
        }
    }
}
