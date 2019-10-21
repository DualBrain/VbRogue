cls
dotnet publish -r win-x64 -c release
rem dotnet publish /p:PublishSingleFile=true -r win-x64
rem dotnet publish /p:PublishTrimmed=true -r win-x64
rem dotnet publish /p:PublishReadyToRun=true -r win-x64
cd bin\Release\netcoreapp3.0\win-x64\publish
dir
