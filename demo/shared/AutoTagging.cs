using System.Collections.Generic;
using Pulumi;

static class AutoTagging
{
    public static ResourceTransformation ApplyTags(Dictionary<string, string> tags)
    {
        return args =>
        {
            var property = args.Args.GetType().GetProperty("Tags");

            if (property is null)
            {
                return null;
            }

            var existingTags = property.GetValue(args.Args, null) as InputMap<string> ?? new InputMap<string>();
            var newTags = InputMap<string>.Merge(existingTags, tags);

            property.SetValue(args.Args, newTags, null);

            return new ResourceTransformationResult(args.Args, args.Options);
        };
    }

    public static ResourceTransformation IgnoreTagModifications(string tagName)
    {
        return args =>
        {
            var existingIgnoreList = args.Options.IgnoreChanges ?? new List<string>();
            existingIgnoreList.Add($"tags.{tagName}");
            args.Options.IgnoreChanges = existingIgnoreList;

            return new ResourceTransformationResult(args.Args, args.Options);
        };
    }

    private static InputMap<string> GetExistingTags(object? value) => value switch
    {
        Dictionary<string, string> tags => (InputMap<string>)tags,
        InputMap<string> tags => tags,
        _ => new InputMap<string>()
    };
}
