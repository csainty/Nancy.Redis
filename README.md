# Nancy.Redis
Support for storing Nancy sessions in Redis.

### Warning!!
This is a work in progress that is being released for early feedback.

## Generic Bits
Rather than just implement a straight session provider for Redis, I have built a generic `KeyValueStoreSessions` class which takes care of all the Nancy pipelines and cookies for you.

```
public class Bootstrapper : DefaultNancyBootstrapper {
  protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
    IKeyValueStore store = ...; // Simple persistence layer
    Nancy.Session.KeyValueStoreSessions.Enable(pipelines, store);
  }
}
```

You can then swap in any `IKeyValueStore` implementation you like.

## Nancy.Redis
Nancy.Redis comes with a single implementation of `IKeyValueStore` that talks to a Redis server using the `ServiceStack.Redis` library.

## Lots to do!
* Real world testing
* Consider lifetime and threading of `IKeyValueStore`
* Consider the best way to create the `IRedisClient` instances
* Is `IKeyValueStore` the best idea or should it be `ISessionStore`?
