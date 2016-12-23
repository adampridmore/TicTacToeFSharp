module TictactoeComputerPlayer
open Tictactoe

let getMove (game:Game) = 
    game 
    |> getLegalMoves 
    |> Seq.head
    
