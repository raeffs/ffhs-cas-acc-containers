using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pulumi;

public class AutoRegistrationStack : Pulumi.Stack
{
    private static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();

    public static TResource GetResource<TResource>()
    {
        return (TResource)GetOrCreateInstance(typeof(TResource));
    }

    protected static object GetOrCreateInstance(Type resourceType)
    {
        if (Instances.ContainsKey(resourceType))
        {
            return Instances[resourceType];
        }

        var constructor = resourceType
            .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
            .Where(c => c.IsPrivate && !c.GetParameters().Any())
            .FirstOrDefault();

        if (constructor is null)
        {
            throw new Exception($"Resource {resourceType.FullName} does not have a parameterless private constructor!");
        }

        var instance = constructor.Invoke(null);
        Instances[resourceType] = instance;
        return instance;
    }

    public AutoRegistrationStack(StackOptions options)
        : base(options)
    { }
}

public class AutoRegistrationStack<TStack> : AutoRegistrationStack
{
    public AutoRegistrationStack(AutoRegistrationStackOptions options)
        : base(GetOptions(options))
    {
        var entryType = typeof(TStack);
        var resourceTypes = entryType.Assembly.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(ResourceInstanceAttribute), false).Any());

        foreach (var type in resourceTypes)
        {
            GetOrCreateInstance(type);
        }
    }

    private static StackOptions GetOptions(AutoRegistrationStackOptions options) => new StackOptions
    {
        ResourceTransformations =
        {
            AutoResourceGroupAssignment.SetResourceGroup(),
            AutoTagging.ApplyTags(new()
            {
                { "ApplicationName", options.ApplicationName },
            })
        }
    };
}
