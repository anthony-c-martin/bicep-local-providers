#!/bin/bash
set -e

usage="Usage: ./publish.sh <repo_name> <version>"
repo_name=${1:?"Missing repo_name. ${usage}"}
version=${2:?"Missing version. ${usage}"}

root=$(dirname ${BASH_SOURCE[0]})
ext_name="bicep-ext-http"
repo_path="providers/http"

env GOOS=darwin GOARCH=arm64 go build -C $root -o "build/http-osx-arm64"
env GOOS=linux GOARCH=amd64 go build -C $root -o "build/http-linux-x64"
env GOOS=windows GOARCH=amd64 go build -C $root -o "build/http-win-x64.exe"

# publish to the registry
~/.azure/bin/bicep publish-provider \
  "$root/types/index.json" \
  --ext-osx-arm64 "$root/build/http-osx-arm64" \
  --ext-linux-x64 "$root/build/http-linux-x64" \
  --ext-win-x64 "$root/build/http-win-x64.exe" \
  --target br:$repo_name.azurecr.io/$repo_path:$version \
  --force