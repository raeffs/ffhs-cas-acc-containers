using Pulumi;

class Stack : AutoRegistrationStack<Stack>
{
    [Output]
    public Output<string> ContainerInstanceAddress { get; set; }

    [Output]
    public Output<string> AppServiceAddress { get; set; }

    [Output]
    public Output<string> ContainerAppAddress { get; set; }

    public Stack()
        : base(GetOptions())
    {
        this.ContainerInstanceAddress = GetResource<ContainerInstance>().IpAddress.Apply(ipAddress => ipAddress!.Fqdn);
        this.AppServiceAddress = GetResource<AppService>().DefaultHostName;
        this.ContainerAppAddress = GetResource<ContainerApp>().Configuration.Apply(config => config!.Ingress!.Fqdn);
    }

    private static AutoRegistrationStackOptions GetOptions() => new AutoRegistrationStackOptions
    {
        ApplicationName = "Containers on Azure Demo",
    };
}
