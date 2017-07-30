# publish console app
rm -rf ./publish && 
cd ../ && 
dotnet restore && 
cd ./console_app && 
dotnet publish -c Release -o ./publish