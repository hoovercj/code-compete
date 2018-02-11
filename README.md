# Code Compete

A platform for code competitions where programmable "players" compete in a variety of online games.

## Plan

* Step 1: Write a .net core implementation of tic-tac-toe based on the concept of a "game" with "players"
    * A game simply defines rules for moving between states in a finite state machine.
    * Given a state, it can make additional state changes by either:
        * Passing the state to a player and returning a new state. It should then validate the state to ensure it is valid within the rules of the game.
        * Invoking an action on a player according to an interface for players of that game and then processing the result. Again, it should then validate the state to ensure it is valid within the rules of the game.
    * The game ends when the state results in a "game over" state.
    * Players will receive a list of all previous states when the game requests an action/new state from them.
    * All functions must be async compatible to allow for file system access, web requests, etc.

* Step 2: Write a generic "game service" that given a "game" and "players", can play a game
    * 2.1: Make this work with the original game and players from step 1
    * 2.2: Push one of the players to a lambda function and have the local player face the lambda player
    * 2.3: Push the game to lambda and have a local player face a lambda player
    * Depending on how lambda actually works, if getting results back as part of a request/response cycle doesn’t work, then it may need to be event driven instead of procedural.  That would mean that state storage may need abstracted and it might be stored in S3 or a database and modifications to that would notify the server or lambda to trigger appropriate functions. In this case only the game function would modify the storage.

* Step 3: Make this a server application that has an endpoint that can start games between defined games. It’s OK if the needed information is manually added to a database to make it work.


If all of these things work, then the next steps will become clear :-)


