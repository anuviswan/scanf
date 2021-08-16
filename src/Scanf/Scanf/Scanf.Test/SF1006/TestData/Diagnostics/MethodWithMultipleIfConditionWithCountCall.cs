using System.Linq;

namespace Scanf.Test
{
    public class MethodWithMultipleIfConditionWithCountCall
    {
        public void Foo()
        {
            var collection = Enumerable.Range(1, 100);

            if (collection.Count() >= 0 && collection.Count() == 0)
            {

            }
        }
    }
}
