using CS = Pulumi.AzureNative.ContainerService;

class KubernetesCluster : CS.ManagedCluster
{
    private KubernetesCluster()
        : base("ffhs-acc-aks", GetArgs())
    { }

    private static CS.ManagedClusterArgs GetArgs() => new CS.ManagedClusterArgs
    {
        ResourceName = "ffhs-acc-aks",
        DnsPrefix = "ffhs-acc-aks",
        EnableRBAC = false,
        KubernetesVersion = "1.20.9",
        Identity = new CS.Inputs.ManagedClusterIdentityArgs
        {
            Type = CS.ResourceIdentityType.SystemAssigned
        },
        NetworkProfile = new CS.Inputs.ContainerServiceNetworkProfileArgs
        {
            LoadBalancerSku = CS.LoadBalancerSku.Standard,
            NetworkPlugin = CS.NetworkPlugin.Kubenet,
        },
        ApiServerAccessProfile = new CS.Inputs.ManagedClusterAPIServerAccessProfileArgs
        {
            EnablePrivateCluster = false
        },
        AgentPoolProfiles = new CS.Inputs.ManagedClusterAgentPoolProfileArgs
        {
            Name = "default",
            Mode = CS.AgentPoolMode.System,
            EnableAutoScaling = false,
            Count = 5,
            MaxPods = 110,
            OsType = CS.OSType.Linux,
            Type = CS.AgentPoolType.VirtualMachineScaleSets,
            VmSize = "Standard_B2s",
            AvailabilityZones =
            {
                "1",
                "2",
                "3"
            }
        },
        AddonProfiles =
        {
            { "azurepolicy", new CS.Inputs.ManagedClusterAddonProfileArgs { Enabled = false, } },
            { "httpApplicationRouting", new CS.Inputs.ManagedClusterAddonProfileArgs { Enabled = true, } },
        },
    };
}
