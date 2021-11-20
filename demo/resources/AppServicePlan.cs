using Web = Pulumi.AzureNative.Web;

class AppServicePlan : Web.AppServicePlan
{
    private AppServicePlan()
        : base("ffhs-acc-app-service-plan-", GetArgs())
    { }

    private static Web.AppServicePlanArgs GetArgs() => new Web.AppServicePlanArgs
    {
        Kind = "linux",
        Reserved = true,
        Sku = new Web.Inputs.SkuDescriptionArgs
        {
            Capacity = 1,
            Name = "B1",
            Tier = "Basic",
        }
    };
}
