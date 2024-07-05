#!/bin/bash
set -e

root=$(dirname ${BASH_SOURCE[0]})

pushd "$root/../.." >/dev/null
protoc --go_out=./src/http --go-grpc_out=./src/http src/extension.proto
popd >/dev/null