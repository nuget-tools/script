cwd=`pwd`
ts=`date "+%Y.%m%d.%H%M.%S"`
#version="${ts}-beta"
version="${ts}"
sed -i -e "s/<Version>.*<\/Version>/<Version>${version}<\/Version>/g" script.csproj
rm -rf obj bin ./*.nupkg
dotnet pack -c Release -o . script.csproj
