using Web = Pulumi.AzureNative.Web;

class AppServicePlan : Web.AppServicePlan
{
    private AppServicePlan()
        : base("ffhs-acc-app-service-plan", GetArgs())
    { }

    private static Web.AppServicePlanArgs GetArgs() => new Web.AppServicePlanArgs
    {
        Name = "ffhs-acc-app-service-plan",
        Kind = "linux",
        Reserved = true,
        Sku = new Web.Inputs.SkuDescriptionArgs
        {
            Capacity = 2,
            Name = "P1V3",
            Tier = "PremiumV3",
        }
    };
}
