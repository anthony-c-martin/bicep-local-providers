#!/bin/bash
set -e

usage="Usage: ./publish.sh <repo_name> <version>"
repo_name=${1:?"Missing repo_name. ${usage}"}
version=${2:?"Missing version. ${usage}"}

root=$(dirname ${BASH_SOURCE[0]})
ext_name="bicep-ext-kubernetes"
repo_path="providers/kubernetes"

# build various flavors
dotnet publish --configuration release --self-contained true -r osx-arm64 $root
dotnet publish --configuration release --self-contained true -r linux-x64 $root
dotnet publish --configuration release --self-contained true -r win-x64 $root

# publish to the registry
~/.azure/bin/bicep publish-provider \
  "$root/types/index.json" \
  --ext-osx-arm64 "$root/bin/release/net8.0/osx-arm64/publish/$ext_name" \
  --ext-linux-x64 "$root/bin/release/net8.0/linux-x64/publish/$ext_name" \
  --ext-win-x64 "$root/bin/release/net8.0/win-x64/publish/$ext_name.exe" \
  --target br:$repo_name.azurecr.io/$repo_path:$version \
  --force