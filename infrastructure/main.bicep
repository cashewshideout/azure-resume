param location string = resourceGroup().location
param appserviceplanname string
param appname string
param cosmosdbname string
param cosmosdatabasename string
param storageaccountname string
param appserviceplansku string = 'Y1'

//log analytics
//application insights

module cosmosaccount 'resources/cosmosdb.bicep' = {
  name: 'cosmosdb'
  params: {
    location: location
    cosmosaccountname: cosmosdbname
    cosmosdatabasename: cosmosdatabasename
  }
}

module storageaccount 'resources/storageaccount.bicep' = {
  name: 'storageaccount'
  params: {
    storagename: storageaccountname
    location: location
  }
}

module appserviceplan 'resources/appserviceplan.bicep' = {
  name: 'appserviceplan'
  params: {
    location: location
    skuName: appserviceplansku
    appServicePlanName: appserviceplanname
  }
}

module functionapp 'resources/functionapp.bicep' = {
  name: 'functionapp'
  params: {
    location: location
    appName: appname
    appserviceplanid: appserviceplan.outputs.id
    storageaccountid: storageaccount.outputs.id
    cosmosdbconnectionstring: cosmosaccount.outputs.connectionString
  }
}
