# CliGameService

## Quick Start
To publish the tic tac toe game and run this service:

```
../scripts/publishTicTacToe.ps1
dotnet run
```

## Summary

CliGameService will look in a subdirectory from where it is run called `Games/` (can be overridden by passing a path). Each subdirectory in `Games/` should have a subdirectory called `Game` with an `exe` for the game, and subdirectories for all players capable of playing the game, each with their own exe.

CliGameService will ask users to pick a game and then pick two players for that game. The game and player processes will be spawned and state is passed between them by writing to files.

## Adding games and players

First a game must be registered, then its players can be registered. The process may differ as more languages are supported, but for now only dotnet is supported.

For dotnet games, the game template is copied  to `../GameProjects/Dotnet/GameName/Game` and the game file is copied into the template from its source location. This allows the game to be available to children projects. From there, the game is "published" to generate an exe and the the output files are copied to `Games/GameName/Game`.

The process is similar for players. The player template is copied to `../GameProjects/Dotnet/GameName/Players/PlayerName` and the player file is copied into the template from its source location with a reference to the SDK and Game library properly referenced. Then, just like the game, it is "published" to generate an exe and the output files are copied to `Games/GameName/Players/PlayerName`.

Example below:
    CodeCompeteDotNet.CliGameService/
    |---- Games/
    |     |---- Game1/
    |     |     |---- Game/
    |     |     |---- Players/
    |     |     |     |---- Player1/
    |     |     |     |---- Player2/
    |     |---- Game2/
    |     |     |---- Game/
    |     |     |---- Players/
    |     |     |     |---- Player1/
    |     |     |     |---- Player2/
    GameProjects/
    |---- Dotnet
          |---- Games/
                |---- Game1/
                |     |---- Game/
                |     |---- Players/
                |     |     |---- Player1/
                |     |     |---- Player2/
                |---- Game2/
                      |---- Game/
                      |---- Players/
                            |---- Player1/
                            |---- Player2/

## Architecture

GamePlayer:
    - doMove(state); : one function that takes a state object

PlayerHost:
    - Written in every supported language.
    - Matches the language of the player
    - main(playerId, statePath, outPath) { writeFile(outPath, player.doMove(parse(readFile(statePath))));}
    - Built as an executable file / module / etc. that receives the state when it is called
    - References the player code (either inlines the code or imports it somehow)
    - Passes the state to the player code and serializes the result out to a file

PlayerProxy:
    - Written in every supported language.
    - Matches the language of the game
    - doMove(exePath, args) : one function that takes a path to a player exe and the args needed to execute it (state, etc.)
    - Spawns the actual PlayerHost process and waits for the output from stdio to return to the caller

Game:
    - Written in every supported language
    - Matches the language of the game
    - Game language matrix may be smaller than player language matrix
    - Takes a list of players, plays the game with them

Game Host:
    - Written in every supported language
    - Matches the language of the game
    - Takes a list of players somehow (config file, main args, etc.)
    - Instantiates PlayerWrappers for the players
    - Passes players to the game, calls "PlayGame", and handles the results

Game Service:
    - A method that executes a game host
    - Could be cli, console, web, etc.
    - First: dotnet console service
        * launches and looks in sub-directories for games and players
        * Provides them as options to choose from the cli
        Games
