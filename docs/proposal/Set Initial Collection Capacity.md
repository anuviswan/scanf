## Set Initial Colection capacity

C# Collections like `List<T>` is implemented with an `array` in the background. The size of `array` would grow (array is resized) as items are added in the collection. Following code shows typical implimentation of the `List.Add()` method.

```csharp
public void Add(T item)
{
    if (_size == _items.Length) EnsureCapacity(_size + 1);
    _items[_size++] = item;
    _version++;
}

private void EnsureCapacity(int min)
{
    if (_items.Length < min) {
        int newCapacity = _items.Length == 0? _defaultCapacity : _items.Length * 2;

        if ((uint)newCapacity > Array.MaxArrayLength) newCapacity = Array.MaxArrayLength;
        if (newCapacity < min) newCapacity = min;
        Capacity = newCapacity;
    }
}

public int Capacity
{
    get {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return _items.Length;
    }
    set {
        if (value < _size) {
            ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
        }
        Contract.EndContractBlock();

        if (value != _items.Length) {
            if (value > 0) {
                T[] newItems = new T[value];
                if (_size > 0) {
                    Array.Copy(_items, 0, newItems, 0, _size);
                }
                _items = newItems;
            }
            else {
                _items = _emptyArray;
            }
        }
    }
}
```

Every time the size of array hits the capacity, the capacity is doubled and the Array is recreated with the new Capacity. All the elements of the previous copy of array is then copied over. This is an expensive operation, if occured too many times. Also, since the capacity is being doubled, there is every possiblity that the array could end up with a little than half its assigned memory unallocated.

The better way to make use of collections would be to intialize it with a likely size during construction.

**Description**: Initialize Collection with a likely capacity

**Category**: Code Smell

**Tags** : Collection, Memory Management

**Non-Complaint Code**

```csharp
var persons = GetPersonList();
var studentList = new List<Student>();
foreach(var person in persons)
{
    studentList.Add(new Student
    {
        FirstName = person.FirstName,
        LastName = person.LastName
    });
}
```

**Fix**

```csharp
var persons = GetPersonList();
var studentList = new List<Student>(persons.Count());
foreach(var person in persons)
{
    studentList.Add(new Student
    {
        FirstName = person.FirstName,
        LastName = person.LastName
    });
}
```
