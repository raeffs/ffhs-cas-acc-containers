using Pulumi;

class Stack : AutoRegistrationStack<Stack>
{
    [Output]
    public Output<string> ClusterName { get; set; }

    public Stack()
        : base(GetOptions())
    {
        this.ClusterName = GetResource<KubernetesCluster>().Name;
    }

    private static AutoRegistrationStackOptions GetOptions() => new AutoRegistrationStackOptions
    {
        ApplicationName = "Containers on Azure Demo",
    };
}
