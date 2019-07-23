#!/bin/sh
dotnet restore
dotnet lambda package --configuration Release --framework netcoreapp2.2 --output-package bin/Release/netcoreapp2.2/deploy-package.zip