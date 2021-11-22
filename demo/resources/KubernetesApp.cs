using Pulumi.Kubernetes.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Core.V1;
using static AutoRegistrationStack;

[ResourceInstance]
class HelloAppService : Service {

    private HelloAppService()
        : base("ffhs-acc-aks-service-", GetArgs(), GetOptions())
    {}

    private static ServiceArgs GetArgs() => new ServiceArgs
    {
        Metadata = new ObjectMetaArgs
        {
            Name = "hello",
            Annotations =
            {
                { "service.beta.kubernetes.io/azure-dns-label-name", "ffhs-acc-aks" }
            }
        },
        Spec = new ServiceSpecArgs
        {
            Type = ServiceSpecType.LoadBalancer,
            Ports = new ServicePortArgs
            {
                Port = 80
            },
            Selector =
            {
                { "app", "hello-app" }
            }
        }
    };

    private static Pulumi.CustomResourceOptions GetOptions() => new Pulumi.CustomResourceOptions
    {
        DependsOn = GetResource<KubernetesCluster>()
    };
}

[ResourceInstance]
class HelloAppDeploymentV1 : HelloAppDeployment {

    private HelloAppDeploymentV1()
        : base(1)
    {}
}

[ResourceInstance]
class HelloAppDeploymentV2 : HelloAppDeployment {

    private HelloAppDeploymentV2()
        : base(2)
    {}
}

class HelloAppDeployment : Deployment {

    protected HelloAppDeployment(int version)
        : base($"ffhs-acc-aks-deployment-v{version}-", GetArgs(version), GetOptions())
    {}

    private static DeploymentArgs GetArgs(int version) => new DeploymentArgs
    {
        Metadata = new ObjectMetaArgs
        {
            Name = $"hello-v{version}"
        },
        Spec = new DeploymentSpecArgs
        {
            Replicas = 3,
            Selector = new LabelSelectorArgs
            {
                MatchLabels =
                {
                    { "app", "hello-app" }
                }
            },
            Template = new PodTemplateSpecArgs
            {
                Metadata = new ObjectMetaArgs
                {
                    Labels =
                    {
                        { "app", "hello-app" },
                        { "version", $"v{version}" }
                    }
                },
                Spec = new PodSpecArgs
                {
                    Containers = new ContainerArgs
                    {
                        Name = "hello",
                        Image = $"docker.io/raeffs/hello:version-{version}",
                        Ports = new ContainerPortArgs
                        {
                            ContainerPortValue = 80
                        },
                        Env = new EnvVarArgs
                        {
                            Name = "ENVIRONMENT_NAME",
                            Value = "Azure Kubernetes Service"
                        }
                    }
                }
            }
        }
    };

    private static Pulumi.CustomResourceOptions GetOptions() => new Pulumi.CustomResourceOptions
    {
        DependsOn = GetResource<KubernetesCluster>()
    };
}
