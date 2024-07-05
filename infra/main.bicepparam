using 'main.bicep'

param acrConfig = {
  name: readEnvironmentVariable('EXTENSIONS_REGISTRY_NAME')
  location: 'East US 2'
  sku: 'Standard'
  anonymousPullEnabled: true
}
