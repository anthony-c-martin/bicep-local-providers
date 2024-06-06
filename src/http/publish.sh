#!/bin/bash
set -e

usage="Usage: ./publish.sh <target>"
target=${1:?"Missing target. ${usage}"}

root=$(dirname ${BASH_SOURCE[0]})
ext_name="bicep-ext-http"

env GOOS=darwin GOARCH=arm64 go build -C $root -o "build/http-osx-arm64"
env GOOS=linux GOARCH=amd64 go build -C $root -o "build/http-linux-x64"
env GOOS=windows GOARCH=amd64 go build -C $root -o "build/http-win-x64.exe"

# publish to the registry
~/.azure/bin/bicep publish-provider \
  "$root/types/index.json" \
  --bin-osx-arm64 "$root/build/http-osx-arm64" \
  --bin-linux-x64 "$root/build/http-linux-x64" \
  --bin-win-x64 "$root/build/http-win-x64.exe" \
  --target "$target" \
  --force