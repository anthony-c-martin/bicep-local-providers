using 'main.bicep'

param acrConfig = {
  name: readEnvironmentVariable('PROVIDERS_REGISTRY_NAME')
  location: 'East US 2'
  sku: 'Standard'
  anonymousPullEnabled: true
}
