## Abstract Class with only abstract methods can be interfaces

:bookmark:_class, Clean Code_

**Category**: Code Smell

**Code**: SF1003

**Description**: Abstract class with only abstract methods can be removed.

When designing abstract classes, if the class end up with _only_ abstract methods, consideration should be taken when to use an interface instead.

**Non-Complaint Code**

```csharp
public abstract class Foo
{
    public abstract void Bar();
    public abstract int GetBar();
}
```

**Fix**

```csharp
public interface IFoo
{
    void Bar();
    int GetBar();
}
```
