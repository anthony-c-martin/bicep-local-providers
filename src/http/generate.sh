#!/bin/bash
set -e

root=$(dirname ${BASH_SOURCE[0]})

pushd "$root/.." >/dev/null
protoc --go_out=./http --go-grpc_out=./http Bicep.Extension/*.proto
popd >/dev/null