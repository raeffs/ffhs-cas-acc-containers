using static AutoRegistrationStack;
using Web = Pulumi.AzureNative.Web;

class ContainerApp : Web.ContainerApp
{
    private ContainerApp()
        : base("ffhs-acc-container-app", GetArgs())
    { }

    private static Web.ContainerAppArgs GetArgs() => new Web.ContainerAppArgs
    {
        Name = "ffhs-acc-container-app",
        KubeEnvironmentId = GetResource<ContainerAppEnvironment>().Id,
        Template = new Web.Inputs.TemplateArgs
        {
            Containers = new Web.Inputs.ContainerArgs
            {
                Name = "hello",
                Image = "docker.io/raeffs/hello:version-1",
                Resources = new Web.Inputs.ContainerResourcesArgs
                {
                    Cpu = 0.25,
                    Memory = ".5Gi"
                },
                Env = new Web.Inputs.EnvironmentVarArgs
                {
                    Name = "ENVIRONMENT_NAME",
                    Value = "Azure Container App"
                }
            },
            RevisionSuffix = "version-1",
            Scale = new Web.Inputs.ScaleArgs
            {
                MinReplicas = 2,
                MaxReplicas = 5,
                Rules = new Web.Inputs.ScaleRuleArgs
                {
                    Name = "default",
                    Http = new Web.Inputs.HttpScaleRuleArgs
                    {
                        Metadata =
                        {
                            { "concurrentRequests", "2" }
                        }
                    }
                }
            }
        },
        Configuration = new Web.Inputs.ConfigurationArgs
        {
            Ingress = new Web.Inputs.IngressArgs
            {
                TargetPort = 80,
                External = true
            }
        }
    };
}
