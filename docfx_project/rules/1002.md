## Avoid Empty Methods

:bookmark:_Methods, Clean Code_

**Category**: Code Smell

**Code**: SF1002

**Description**: Empty Methods should be avoided.

Empty methods in the code base could be avoided

**Non-Complaint Code**

```csharp
public void Foo()
{
}

```

**Code Fix**

Code Fix with `NotImplementedException` raised.

```csharp
public void Foo()
{
    throw new NotImplementedException();
}
```
