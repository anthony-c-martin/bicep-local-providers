#!/bin/bash
set -e

# This script creates the necessary registry infrastructure and configures GitHub OpenID Connect to allow
# GitHub actions to push to the registry in its CD pipeline.
usage="Usage: ./initial_setup.sh <tenantId> <subscriptionId>"
tenantId=${1:?"Missing tenantId. ${usage}"}
subId=${2:?"Missing subscriptionId. ${usage}"}

repoOwner="anthony-c-martin"
repoName="bicep-local-providers"
rgName="biceplocaldeploy"
rgLocation="East US 2"

az account set -n "$subId"
az group create \
  --location "$rgLocation" \
  --name "$rgName" >/dev/null

appCreate=$(az ad app create --display-name $rgName)
appId=$(echo $appCreate | jq -r '.appId')

if [[ -z $(az ad sp show --id $appId 2>/dev/null) ]]; then
  az ad sp create --id $appId >/dev/null
fi

spId=$(az ad sp show --id $appId --query id --output tsv)
az role assignment create \
  --role owner \
  --subscription $subId \
  --assignee-object-id $spId \
  --assignee-principal-type ServicePrincipal \
  --scope /subscriptions/$subId/resourceGroups/$rgName >/dev/null

repoSubject="repo:$repoOwner/$repoName:ref:refs/heads/main"
if [[ -z $(az ad app federated-credential show --id $appId --federated-credential-id $repoName 2>/dev/null) ]]; then
  az ad app federated-credential create \
    --id $appId \
    --parameters '{"name":"'$repoName'","issuer":"https://token.actions.githubusercontent.com","subject":"'$repoSubject'","description":"GitHub OIDC Connection","audiences":["api://AzureADTokenExchange"]}' >/dev/null
fi

gh -R $repoOwner/$repoName secret set EXTENSIONS_CLIENT_ID --body $appId
gh -R $repoOwner/$repoName secret set EXTENSIONS_SUBSCRIPTION_ID --body $subId
gh -R $repoOwner/$repoName secret set EXTENSIONS_TENANT_ID --body $tenantId