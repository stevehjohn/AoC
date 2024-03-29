rm *.zip
dotnet publish ./AoC.Games/AoC.Games.csproj -c Release -r osx-x64 /p:PublishReadyToRun=true /p:TieredCompilation=false --self-contained /p:PublishReadyToRunShowWarnings=true
dotnet publish ./AoC.Games/AoC.Games.csproj -c Release -r osx-arm64 /p:PublishReadyToRun=true /p:TieredCompilation=false --self-contained /p:PublishReadyToRunShowWarnings=true
dotnet publish ./AoC.Games/AoC.Games.csproj -c Release -r win-x64 /p:PublishReadyToRun=true /p:TieredCompilation=false --self-contained /p:PublishReadyToRunShowWarnings=true
cd ./AoC.Games/bin/Release/net8.0/osx-x64/publish
pwd
chmod +xx AoC.Games
rm *.zip
zip -r AoC.Games.macOS.Intel.zip *
cd -
mv ./AoC.Games/bin/Release/net8.0/osx-x64/publish/AoC.Games.macOS.Intel.zip .

cd ./AoC.Games/bin/Release/net8.0/osx-arm64/publish
pwd
chmod +xx AoC.Games
rm *.zip
zip -r AoC.Games.macOS.Apple.zip *
cd -
mv ./AoC.Games/bin/Release/net8.0/osx-arm64/publish/AoC.Games.macOS.Apple.zip .

cd ./AoC.Games/bin/Release/net8.0/win-x64/publish
pwd
rm *.zip
zip -r AoC.Games.Windows.Intel.zip *
cd -
mv ./AoC.Games/bin/Release/net8.0/win-x64/publish/AoC.Games.Windows.Intel.zip .
