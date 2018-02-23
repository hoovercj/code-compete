$ProjectDirRoot = "GameProjects\Dotnet\"

function Get-ProjectDirForGame
{
Param(
    [Parameter(Mandatory=$true)][string] $GameName
)
    "$ProjectDirRoot\$GameName\Game"
}

function Get-ProjectDirForPlayer
{
Param(
    [Parameter(Mandatory=$true)][string] $GameName,
    [Parameter(Mandatory=$true)][string] $PlayerName
)
    "$ProjectDirRoot\$GameName\Players\$PlayerName"
}

function Publish-GameToCliService
{
Param(
    [Parameter(Mandatory=$true)][string] $Path,
    [Parameter(Mandatory=$true)][string] $GameName
)
    $GameProjectDir = Get-ProjectDirForGame $GameName
    $GamePublishDir = "$GameProjectDir\bin\Release\netcoreapp2.0\win10-x64\publish"
    $GameOutDir = "CodeCompete.CliGameService\Games\$GameName\Game"

    if (Test-Path -Path $GameProjectDir)
    {
        Write-Host "Deleting existing game..."
        Remove-Item -Path $GameProjectDir -Recurse -Force
    }

    Write-Host "Creating new template..."
    dotnet new codecompetegame -o $GameProjectDir --GameName $GameName --force
    Write-Host "Copying $Path\Game.cs to $GameProjectDir\Game.cs..."
    Copy-Item -Path "$Path\Game.cs" -Destination "$GameProjectDir\Game.cs" -Force
    Write-Host "Copying $Path\Move.cs to $GameProjectDir\Move.cs..."
    Copy-Item -Path "$Path\Move.cs" -Destination "$GameProjectDir\Move.cs" -Force
    Write-Host "Generating exe..."
    dotnet publish "$GameProjectDir\CodeCompeteDotNet.GameHost.csproj" -c Release -r win10-x64
    Write-Host "Copying $GamePublishDir to $GameOutDir..."
    Copy-Item -Force -Recurse -Path $GamePublishDir -Destination $GameOutDir

    # Remove-Item $GameProjectDir -Recurse
}

function Publish-PlayerToCliService
{
Param(
    [Parameter(Mandatory=$true)][string] $Path,
    [Parameter(Mandatory=$true)][string] $GameName,
    [Parameter(Mandatory=$true)][string] $PlayerName
)
    $GameProjectDir = Get-ProjectDirForGame $GameName

    $PlayerProjectDir = Get-ProjectDirForPlayer $GameName $PlayerName
    $PlayerPublishDir = "$PlayerProjectDir\bin\Release\netcoreapp2.0\win10-x64\publish"
    $PlayerOutDir = "CodeCompete.CliGameService\Games\$GameName\Players\$PlayerName"

    Write-Host "Creating new template..."
    dotnet new codecompeteplayer -o $PlayerProjectDir --PlayerName $PlayerName --GameName $GameName --force
    Write-Host "Copying $Path\Player.cs to $PlayerProjectDir\Player.cs..."
    Copy-Item -Path "$Path\Player.cs" -Destination "$PlayerProjectDir\Player.cs" -Force
    Write-Host "Copying $GameProjectDir\Move.cs to $PlayerProjectDir\Move.cs..."
    Copy-Item -Path "$GameProjectDir\Move.cs" -Destination "$PlayerProjectDir\Move.cs" -Force
    Write-Host "Generating exe..."
    dotnet publish "$PlayerProjectDir\CodeCompeteDotNet.PlayerHost.csproj" -c Release -r win10-x64
    Write-Host "Copying $PlayerPublishDir to $PlayerOutDir..."
    Copy-Item -Force -Recurse -Path $PlayerPublishDir -Destination $PlayerOutDir

    # Remove-Item $PlayerProjectDir -Recurse
}