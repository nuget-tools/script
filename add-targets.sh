#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=`pwd`
ts=`date "+%Y.%m%d.%H%M.%S"`

dotnet new console --name Targets
cd Targets
dotnet add package Bullseye
dotnet add package SimpleExec
