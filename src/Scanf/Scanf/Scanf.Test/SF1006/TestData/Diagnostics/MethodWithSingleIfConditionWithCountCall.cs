using System.Linq;

namespace Scanf.Test
{
    public class MethodWithSingleIfConditionWithCountCall
    {
        public void Foo()
        {
            var collection = Enumerable.Range(1, 100);

            if(collection.Count() >= 0)
            {

            }
        }
    }
}
