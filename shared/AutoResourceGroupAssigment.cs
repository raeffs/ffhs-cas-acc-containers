using Pulumi;

static class AutoResourceGroupAssignment
{
    public static ResourceTransformation SetResourceGroup()
    {
        return args =>
        {
            var config = new Config();

            var property = args.Args.GetType().GetProperty("ResourceGroupName");

            if (property is null)
            {
                return null;
            }


            property.SetValue(args.Args, (Input<string>)config.Require("resourceGroupName"), null);

            return new ResourceTransformationResult(args.Args, args.Options);
        };
    }
}
