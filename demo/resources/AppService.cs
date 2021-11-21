using static AutoRegistrationStack;
using Web = Pulumi.AzureNative.Web;

class AppService : Web.WebApp
{
    private AppService()
        : base("ffhs-acc-app-service-", GetArgs())
    { }

    private static Web.WebAppArgs GetArgs() => new Web.WebAppArgs
    {
        Name = "ffhs-acc-app-service",
        ServerFarmId = GetResource<AppServicePlan>().Id,
        SiteConfig = new Web.Inputs.SiteConfigArgs
        {
            LinuxFxVersion = "DOCKER|raeffs/hello:version-1",
            AppSettings =
            {
                new Web.Inputs.NameValuePairArgs
                {
                    Name = "DOCKER_REGISTRY_SERVER_URL",
                    Value = "https://index.docker.io"
                },
                new Web.Inputs.NameValuePairArgs
                {
                    Name = "DOCKER_REGISTRY_SERVER_USERNAME",
                    Value = ""
                },
                new Web.Inputs.NameValuePairArgs
                {
                    Name = "DOCKER_REGISTRY_SERVER_PASSWORD",
                    Value = ""
                },
                new Web.Inputs.NameValuePairArgs
                {
                    Name = "WEBSITES_PORT",
                    Value = "8080"
                },
                new Web.Inputs.NameValuePairArgs
                {
                    Name = "ENVIRONMENT_NAME",
                    Value = "Azure App Service"
                }
            },
            AlwaysOn = true
        },
        ClientAffinityEnabled = false
    };
}
