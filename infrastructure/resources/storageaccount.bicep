param storagename string
param location string = resourceGroup().location
param kind string = 'StorageV2'
param skuname string = 'Standard_LRS'

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-06-01' = {
  name: storagename
  location: location
  kind: kind
  sku: {
    name: skuname
  }
}

output id string = storageAccount.id
