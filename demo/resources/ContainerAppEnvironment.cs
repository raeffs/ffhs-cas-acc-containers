using Pulumi;

// creation of container app environment via pulumi seems broken
class ContainerAppEnvironment
{
    private ContainerAppEnvironment() { }

    public Output<string> Id { get; } = GetId();

    private static Output<string> GetId()
    {
        var config = new Config();
        var subscriptionId = config.Require("subscriptionId");
        var resourceGroupName = config.Require("resourceGroupName");
        var containerAppEnvironmentName = config.Require("containerAppEnvironmentName");
        var id = $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/kubeEnvironments/{containerAppEnvironmentName}";
        return Output.Create(id);
    }
}
