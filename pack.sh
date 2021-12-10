#!/bin/sh
version=$1
echo $version

dotnet pack -c Release -p:PackageVersion=$version --no-build --no-restore DotnetVueTemplates.csproj