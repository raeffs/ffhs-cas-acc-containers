using CI = Pulumi.AzureNative.ContainerInstance;

class ContainerInstance : CI.ContainerGroup
{
    private ContainerInstance()
        : base("ffhs-acc-container-instance-", GetArgs())
    { }

    private static CI.ContainerGroupArgs GetArgs() => new CI.ContainerGroupArgs
    {
        Containers =
        {
            new CI.Inputs.ContainerArgs
            {
                Name = "hello",
                Image = "raeffs/hello:version-1",
                Ports =
                {
                    new CI.Inputs.ContainerPortArgs
                    {
                        Port = 80
                    }
                },
                Resources = new CI.Inputs.ResourceRequirementsArgs
                {
                    Requests = new CI.Inputs.ResourceRequestsArgs
                    {
                        Cpu = 0.5,
                        MemoryInGB = 0.5
                    }
                },
                EnvironmentVariables =
                {
                    new CI.Inputs.EnvironmentVariableArgs
                    {
                        Name = "ENVIRONMENT_NAME",
                        Value = "Azure Container Instance"
                    }
                }
            }
        },
        IpAddress = new CI.Inputs.IpAddressArgs
        {
            Type = CI.ContainerGroupIpAddressType.Public,
            DnsNameLabel = "ffhs-acc-container-instance",
            Ports =
            {
                new CI.Inputs.PortArgs
                {
                    Port = 80
                }
            }
        },
        OsType = CI.OperatingSystemTypes.Linux,
    };
}
