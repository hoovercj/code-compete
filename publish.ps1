function Publish-GameToCliService
{
Param(
    [Parameter(Mandatory=$true)][string] $Path,
    [Parameter(Mandatory=$true)][string] $Name
)

    $GameProjectDir = "Temp----$Name"
    $GamePublishDir = "$GameProjectDir\bin\Release\netcoreapp2.0\win10-x64\publish"
    $GameOutDir = "CodeCompete.CliGameService\Games\$Name"

    dotnet new codecompetegame -o $GameProjectDir --GameName $Name --force
    Copy-Item -Path "$Path\Game.cs" -Destination "$GameProjectDir\Game.cs"
    dotnet publish "$GameProjectDir\CodeCompeteDotNet.GameHost.csproj" -c Release -r win10-x64
    Copy-Item -Path $GamePublishDir -Destination $GameOutDir

    # Remove-Item $GameProjectDir -Recurse
}