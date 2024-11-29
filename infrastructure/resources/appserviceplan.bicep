param appServicePlanName string
param location string
param skuName string = 'Y1'

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
    name: appServicePlanName
    location: location
    sku: {
        name: skuName
        tier: 'Dynamic'
    }
}

output id string = appServicePlan.id
