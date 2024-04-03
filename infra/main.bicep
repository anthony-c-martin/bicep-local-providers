param acrConfig {
  name: string
  location: string
  sku: 'Standard' | 'Premium'
  anonymousPullEnabled: bool
}

resource acrResource 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: acrConfig.name
  location: acrConfig.location
  sku: {
    name: acrConfig.sku
  }
  properties: {
    adminUserEnabled: false
    anonymousPullEnabled: acrConfig.anonymousPullEnabled
  }
}

output loginServer string = acrResource.properties.loginServer
