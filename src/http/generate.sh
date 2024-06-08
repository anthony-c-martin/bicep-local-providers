#!/bin/bash
set -e

root=$(dirname ${BASH_SOURCE[0]})

pushd "$root/../.." >/dev/null
protoc --go_out=./src/http --go-grpc_out=./src/http bicep/src/Bicep.Local.Extension/extension.proto
popd >/dev/null