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
```

**Fix**

```csharp
// Remove the empty method
```
