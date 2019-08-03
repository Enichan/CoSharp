# CoSharp

Lua style coroutines for C#, with Unity style waiting.

## Basic usage

CoSharp is based closely on Lua's implementation for coroutines, with some of Unity's waiting functionality thrown in. The simplest way to use this is like this:

```csharp
    IEnumerable<int> SomeCoroutine(int arg1, int arg2) {
        // do some stuff here
        
        // yield back to program
        yield return arg1;
        
        // do some more stuff here
        
        // yield again
        yield return arg2; 
        
        // finish up
    }
   
    // output: 1, 2
    using (var co = Coroutine.Create(SomeCoroutine(1, 2))) {
        while (co.Resume()) {
            Console.WriteLine(co.Result.ToString());
        }
    }
```
    
Coroutines are functions that return IEnumerable&lt;T>, where T can be any type you like. A coroutine can then be created by calling Coroutine.Create with the return value of one of those functions. This creates a Coroutine&lt;T> instance, where T matches the return type of the function. At this point you can call Resume to run the coroutine until the next time it yields, restart the coroutine by calling Reset, and get the Result of the last yield as a CoResult&lt;T> type.

## CoResult&lt;T>

The Result deserves some special attention. You can access it via the coroutine instance's Result property, but it's also returned every time you call Resume. This contains the last return value, exception, and status when the coroutine was run. This is useful to check if execution proceeded as expected: CoResult&lt;T> casts implicitly to a boolean where a value of true indicates that the coroutine is still running, and a value of false indicates it is not, either because it finished or because it encountered an error, in which case the Exception field will be set. Keep in mind that the ReturnValue, Exception, and Status properties of the coroutine instance contain the same values, but the ReturnValue will be of type object instead of type T.

## Coroutine status

A coroutine's status (of type CoStatus) indicates it's current state. This can be any of the following values:

- Suspended: the coroutine has not started yet or isn't finished yet but isn't currently being run
- Waiting: same as Suspended, except the coroutine is paused while waiting (see section on waiting below)
- Running: the coroutine is currently being run
- Dead: the coroutine has stopped, because it finished or encountered an error, running a dead coroutine is an error

## Exceptions

By default coroutines will silently catch exceptions and not re-throw them, instead storing the Exception in its last result. This behavior can be changed by setting the coroutine's ThrowErrors value to true, or setting the 'throwErrors' parameter to true when calling Coroutine.Create.

## Waiting

Like Unity coroutines a coroutine can be made to wait until some amount of time has passed or until some expression is true or false. This can be achieved by calling Coroutine.WaitFor, Coroutine.WaitUntil, or Coroutine.WaitWhile before yielding a value. While in this wait state a coroutine's status will not be Suspended, but will instead be Waiting. For example:

```csharp
    IEnumerable<int> WaitCoroutine() {
        Coroutine.WaitFor(2.0); // set coroutine to wait for 2 seconds after yielding
        yield return 0; // yield value
        // 2 seconds will elapse before getting to the next line
        
        Coroutine.WaitUntil(() => someThing); // set to wait until something is true after yielding
        yield return 0:
        // the next line will be reached when someThing == true
        
        Coroutine.WaitWhile(() => otherThing); // set to wait while some other thing is true after yielding
        yield return 0;
        // the block of code between here and the end of the function will execute when otherThing == false
    }
```
    
## Passing arguments via Resume

Because the method's arguments are set when creating the coroutine they can't change dynamically when Resume is called. However, in Lua, when resuming a coroutine values can be passed in by passing them to the method as arguments. Something similar can be done here by passing an anonymous object to Resume and calling Coroutine.Args inside the coroutine:

```csharp
    IEnumerable<string> CoroutineWithArgs() {
        var args = Coroutine.Args(new { name = string.Empty });
        yield return "hi " + args.name;
        yield return "hello " + Coroutine.Args(ref args).name;
        yield return "sup " + Coroutine.Args(ref args).name;
    }
    
    using (var co = Coroutine.Create(CoroutineWithArgs())) {
        Console.WriteLine(co.Resume(new { name = "Emma" }).ToString());
        Console.WriteLine(co.Resume(new { name = "Jim" }).ToString());
        Console.WriteLine(co.Resume(new { name = "Bob" }).ToString());
    }
    
    // output:
    //    hi Emma
    //    hello Jim
    //    sup Bob
```

Because of anonymous type inference, a placeholder anonymous type with empty values must be passed to Coroutine.Args. For convenience there's a version of this method with a ref parameter, so that an args variable can be set inline and its result used immediately.

## Convenience functions

There's some other static convenience functions on Coroutine. Coroutine.Current returns the currently running coroutine (whichever is at the top of the stack) or null if outside of any coroutines. Coroutine.Wrap wraps a coroutine into a function that can be called normally to resume, which returns a CoResult&lt;T> value, and which can take a single anonymous type argument. Coroutine.MakeSynchronous creates a function wrapper which can be called with the same arguments as the original method, runs until it ends, and returns the last return value. Coroutine.MakeSyncWithArgs does the same thing, but the first argument to the method takes the anonymous resume argument object. Note that coroutines that have been made synchronous will always throw exceptions. For example:

```csharp
    // adds value to either itself or to the provided rhs coroutine argument, count times
    IEnumerable<int> Add(int value, int count) {
        for (int i = 0; i < count; i++) {
            // calling Coroutine.Args with isDefaultValue or keep keepValueIfNull set to true
            // will return the type inference placeholder if no arguments were passed to Resume
            var rhs = Coroutine.Args(new { rhs = value }, true).rhs;
            value += rhs;
            yield return value;
        }
    }
    
    var wrapped = Coroutine.Wrap(Add(2, 5));
    CoResult<int> wrappedResult;
    
    // output: 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048
    while ((wrappedResult = wrapped()) == true) {
        Console.WriteLine(wrappedResult.ReturnValue);
    }
    
    // output: 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
    while ((wrappedResult = wrapped(new { rhs = 1 })) == true) {
        Console.WriteLine(wrappedResult.ReturnValue);
    }

    // output: 2048
    var sync = Coroutine.MakeSynchronous<int, int, int>(Add);
    Console.WriteLine(sync(2, 10));

    // output: 12
    var syncArgs = Coroutine.MakeSyncWithArgs<int, int, int>(Add);
    Console.WriteLine(syncArgs(new { rhs = 1 }, 2, 10));
```
    
## CoroutinePool
    
An easy way to group and resume a collection of simple coroutines is the CoroutinePool class. Add or remove coroutines using the Add and Remove methods, and resume all coroutines in the pool using ResumeAll. Dead coroutines are automatically removed from the pool, and any coroutine which is removed either automatically or by calling Remove is properly disposed. CoroutinePool also has OnError and OnEnd events, which are called for any coroutines that have finished by encountering an error or finishing execution respectively. If Reset is called on a coroutine passed to either of these events it will be kept in the pool and run again.
