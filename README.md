# Code Compete

A platform for code competitions where programmable "players" compete in a variety of online games.

The long-term goal is to have a platform that allows users to submit a "game" written in one of several languages. Users can then submit "players" for that game in the supported language of their choice.

Then users can pit their players against others.

## Try it out

_Prerequisites_: Install the [dotnet core sdk](https://www.microsoft.com/net/learn/get-started/)

```shell
git clone https://github.com/hoovercj/code-compete.git
cd code-compete/ConsoleGameHost
dotnet restore
dotnet run
```

## Architecture

### Game
A "game" is responsible for telling players to make moves, validating their moves, and ending the game. It does not need to know or care about the type of players it has (computer, console, remote, etc.).

It does this by extending AbstractGame (only .net core is supported at the moment) and by defining the shape of its "state". In the case of Tic Tac Toe, the state is a 2-d string array (3x3).

See [TicTacToe.cs](./TicTacToe/TicTacToeDotNet/TicTacToe.cs)

### Player

A "player" very simple to write. Given a copy of the game state (including all previous moves), it must return the next state of the game. In the case of Tic Tac Toe, a player would receive the current board and all previous boards and would return a new board with their next piece placed.

See [SimpleComputerTicTacToePlayer.cs](TicTacToe/SimpleComputerTicTacToePlayer/SimpleComputerTicTacToePlayer.cs) and [ConsoleTicTacToePlayer.cs](TicTacToe/ConsoleTicTacToePlayer/ConsoleTicTacToePlayer.cs).

### Host

A host is the service that orchestrates a game. An example is [ConsoleGameHost/Program.cs](ConsoleGameHost/Program.cs) which is a .net core console app which starts a Tic Tac Toe game with a computer player and a console player.

A host could be made to communicate with players across processes or networks, as well.

## Plan

Below is a tentative (and non-exhaustive) list of steps I'll be taking.

### Short-Term

* [X] Write a .net core implementation of a tic-tac-toe game based on the concept of a "game" with "players" outlined above
* [X] Write a console app "host" that that allows playing the above game
* [ ] Write wrappers for "players" and "games" so that hosts can communicate with players and/or games in different processes, online, etc.
* [ ] Write a good Tic Tac Toe player in Javascript (node.js) and host to communicate with it in a different process

### Long-Term
* [ ] Push one of the players to an aws lambda function and have the local player face the lambda player
* [ ] Create an asp.net core server to act as a host. It should have a database with a list of (manually entered?) aws lambda functions for games and players and allow users to start existing games against existing players
* [ ] Improve the server to allow new games and players to be added directly from the server
* [ ] Add "users"
* [ ] Make it pretty







GamePlayer: (dotnet DONE)
    - doMove(state); : one function that takes a state object

PlayerHost: (dotnet DONE)
    - Written in every supported language.
    - Matches the language of the player
    - main(playerId, statePath, outPath) { writeFile(outPath, player.doMove(parse(readFile(statePath))));}
    - Built as an executable file / module / etc. that receives the state when it is called
    - References the player code (either inlines the code or imports it somehow)
    - Passes the state to the player code and serializes the result out to a file

PlayerProxy: (dotnet DONE)
    - Written in every supported language.
    - Matches the language of the game
    - doMove(exePath, args) : one function that takes a path to a player exe and the args needed to execute it (state, etc.)
    - Spawns the actual PlayerHost process and waits for the output from stdio to return to the caller

Game: (dotnet DONE)
    - Written in every supported language
    - Matches the language of the game
    - Game language matrix may be smaller than player language matrix
    - Takes a list of players, plays the game with them

Game Host: (dotnet DONE)
    - Written in every supported language
    - Matches the language of the game
    - Takes a list of players somehow (config file, main args, etc.)
    - Instantiates PlayerWrappers for the players
    - Passes players to the game, runs the game, and handles the results

Game Service: (dotnet DONE)
    - A method that executes a game host
    - Could be cli, console, web, etc.
    - First: dotnet console service
        * launches and looks in sub-directories for games and players
        * Provies them as options to choose from the cli
        exe
        |---- Game1/
        |     |---- Player1/
        |     |---- Player2/
        |---- Game2/
              |---- Player2/
              |---- Player3/
