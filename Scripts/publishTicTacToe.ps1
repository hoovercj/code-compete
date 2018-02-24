Import-Module $PSScriptRoot\publish.psm1

Install-DotnetTemplates
Publish-GameToCliService -Path TicTacToe\TicTacToeDotNet -GameName TicTacToe
Publish-PlayerToCliService -Path $PSScriptRoot\..\TicTacToe\SimpleComputerTicTacToePlayer -GameName TicTacToe -PlayerName SimpleComputerPlayer