param appName string
param location string
param appserviceplanid string
param storageaccountid string
param cosmosdbconnectionstring string

resource functionApp 'Microsoft.Web/sites@2021-02-01' = {
    name: appName
    location: location
    properties: {
        serverFarmId: appserviceplanid
        siteConfig: {
            appSettings: [
                {
                    name: 'FUNCTIONS_WORKER_RUNTIME'
                    value: 'dotnet'
                }
                {
                    name: 'FUNCTIONS_EXTENSION_VERSION'
                    value: '~4'
                }
                {
                    name: 'AzureWebJobsStorage'
                    value: listKeys(storageaccountid, '2021-06-01').keys[0].value
                }
                {
                    name: 'WEBSITE_RUN_FROM_PACKAGE'
                    value: '1'
                }
                {
                    name: 'AzureResumeConnectionString'
                    value: cosmosdbconnectionstring
                }
            ]
        }
    }
}
