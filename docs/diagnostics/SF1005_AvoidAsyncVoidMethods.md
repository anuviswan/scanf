## Async method should not return void

🔖 Async

**Category** : Code Smell

**Rule Id** : SF1005

**Description** : Async method should not return void

When an exception is thrown in an `async Task` method, the exception is captured and placed in the `Task` object. When an asynchronous method returns `void`, the absence of `Task` object would lead to the exception to be thrown directly on the active SynchronizationContext. Furthermore, `async void` methods are difficult to test.

The sole purpose of `async void` should be in the `eventHandlers`

**Non-Complaint Code**

```
public async void Foo()
{
  await DoBar();
}
```

**Code Fix**

Code Fix raised.

```
public async Task Foo()
{
  await DoBar();
}
```
