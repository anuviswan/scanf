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

**Code Fix**

Code Fix with `NotImplementedException` raised.

```csharp
public void Foo()
{
    throw new NotImplementedException();
}
```

**Update** : 25/11/2020

Extra Scenarios observed.

Scenario 1 : Expression-bodied Metod with Expression.Empty()

Sample Non-Complaint Code
```
public void Foo() => Expression.Empty();
```
`Expresion.Empty()` intends to be used in scenarios where an expresion is expected, but no action is desired. This would point towards an intentional behavior rather than a code smell. At this point, this scenario would be considered a complaint code unless further questions/discusions raise in the topic.

Scenario 2 : Should Empty Methods be removed.

