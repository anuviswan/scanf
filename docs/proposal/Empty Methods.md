## Remove Empty Methods

:bookmark:_Methods, Clean Code_

**Category**: Code Smell

**Code**: SF1002

**Description**: Empty Methods could be removed

Empty methods in the code base could be removed

**Non-Complaint Code**

```csharp
public void Foo()
{
}

public void Foo() => Expression.Empty();
```

**Code Fix**

Code Fix with `NotImplementedException` raised.

```csharp
public void Foo()
{
    throw new NotImplementedException();
}
```

**Update** : 25/11/2020

First version would not provide option to remove the method. Instead, the only fix that would be available would to add exception.

In the next version, two fixes would be provided.

- Remove Empty Method
- Add Exception
