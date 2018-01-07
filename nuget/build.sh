
##
## Build script for creating and publish NuGet package
## 
## @ 2017 Yauheni Pakala
##

NUGET="nuget.exe"
LATEST_NUGET_URL="https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

if [ ! -f $NUGET ]; then
   echo "Downloading '$NUGET' ..."
   curl -o $NUGET $LATEST_NUGET_URL
   echo "Done."
fi

mono $NUGET | grep "Version"

echo "=========================="

NUSPEC=$(ls | grep *.nuspec)

mono $NUGET pack $NUSPEC

echo "=========================="

NUPKG=$(ls | grep *.nupkg)

echo "Do you want to publish '$NUPKG' to NuGet.org? (y/n)"
read confirm

# TODO: add checking for setApiKey before push

if [ "$confirm" == "y" ]; then
	mono $NUGET push $NUPKG -Source https://api.nuget.org/v3/index.json
fi