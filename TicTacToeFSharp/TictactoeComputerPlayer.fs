module TictactoeComputerPlayer
open Tictactoe

let getMove (game:Game) = 
    let myToken = game.NextMove

    let legalMoves = 
        game 
        |> getLegalMoves 
    
    let isWin (state:GameState) = 
       match state with 
       | GameState.XWon when myToken = X -> true
       | GameState.OWon when myToken = O -> true
       | _ -> false
       
    let winningMoves = 
        legalMoves 
        |> Seq.map(fun move -> move, move |> playMove game)
        |> Seq.filter(fun (_ , nextGameStae) -> nextGameStae.State |> isWin)
        |> Seq.map fst

    if (winningMoves |> Seq.isEmpty |> not) 
    then winningMoves |> Seq.head
    else legalMoves |> Seq.head
