#!/bin/sh
version=$1
echo $version

dotnet pack -p:PackageVersion=$version --no-build --no-restore DotnetVueTemplates.csproj