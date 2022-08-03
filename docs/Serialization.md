To enable serialization for specific classes/interfaces, follow these rules:
1. Serializable interfaces and classes must be decorated with `[ProtoContract]` attribute
1. Creating class `Foo` that implementing specific interface `IFoo`, you must add `[ProtoInclude(1, typeof(Foo))]` attribute to `IFoo` interface (replace `1` with lowest int unique  within interface declaration)
1. Creating class `Bar` that inherit specific class `Baz`, you must add `[ProtoInclude(1, typeof(Bar))]` attribute to `Baz` class
