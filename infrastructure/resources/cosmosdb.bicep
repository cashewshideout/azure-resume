param location string = resourceGroup().location
param cosmosaccountname string
param accountoffertype string = 'Standard'

resource cosmosaccount 'Microsoft.DocumentDB/databaseAccounts@2021-06-15' = {
  name: cosmosaccountname
  location: location
  properties: {
    databaseAccountOfferType: accountoffertype
    locations: [
      {
        locationName: location
        failoverPriority: 0
      }
    ]
  }
}

param cosmosdatabasename string

resource sqlDatabase 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2021-06-15' = {
  name: cosmosdatabasename
  parent: cosmosaccount
  properties: {
    resource: {
      id: cosmosdatabasename
    }
  }
}

output connectionString string = cosmosaccount.properties.documentEndpoint
