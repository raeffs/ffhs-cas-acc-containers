# Containers on Azure Demo

## Prerequisites

The following tools need to be installed:

-   Azure CLI
-   Pulumi CLI

## Preparation

To deploy all the resources of the demo project, the following preparations need to be made:

-   The Pulumi CLI needs to be authenticated with `pulumi login`.
-   The Azure CLI needs to be authenticated with `az login`.
-   The target subscription needs to be selected with `az account set --subscription <subscription-id>`.
-   A resource group with the name `ffhs-acc-containers-on-azure` needs to be created in the Azure region `northeurope`.
-   An Azure Container App Environment with the name `ffhs-acc-container-app-env` needs to be created in that resource group. This was not possible with Pulumi at the time of writing.
-   The Kubernetes cluster in the cluster project needs to be deployed by running `pulumi up -f -y` inside the cluster project folder.
-   The kubectl CLI needs to be installed by running `az aks install-cli`.
-   The kubectl CLI need to be authenticated by running `az aks get-credentials --resource-group ffhs-acc-containers-on-azure --name ffhs-acc-aks`.

## Deploy Demo Resources

The resources of the demo project can be deployed by running `pulumi up -f -y` inside the demo project folder.

The FQDNs of the apps are part of the Pulumi output after successful deployment.

Example output:

```
Outputs:
    AppServiceAddress       : "ffhs-acc-app-service.azurewebsites.net"
    ContainerAppAddress     : "ffhs-acc-container-app.grayrock-e7539734.northeurope.azurecontainerapps.io"
    ContainerInstanceAddress: "ffhs-acc-container-instance.northeurope.azurecontainer.io"
    KubernetesAppAddress    : "ffhs-acc-aks.northeurope.cloudapp.azure.com"
```

## Teardown Demo Resources

To resources can be destroyed by running `pulumi destroy -f -y`.
