#set environment variables
export ASPNETCORE_ENVIRONMENT=Development
# publish web
rm -rf ./publish && 
cd ../ && 
dotnet restore && 
cd ./web && 
dotnet publish -c Release -o ./publish