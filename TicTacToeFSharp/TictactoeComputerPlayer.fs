module TictactoeComputerPlayer
open Tictactoe

let tokenToWinnigState = 
    function 
    | X -> XWon
    | O -> OWon

let getMove (game:Game) = 
    let myToken = game.NextMove
    let opponentToken = myToken |> tokenToggle
    let targetWinningState = myToken |> tokenToWinnigState
    let loosingState = myToken |> Tictactoe.tokenToggle |> tokenToWinnigState

    let legalMoves = game |> getLegalMoves 
       
    let winningMoves = 
        legalMoves 
        |> Seq.map(fun move -> move, move |> playMove game)
        |> Seq.filter(fun (_ , nextGameStae) -> nextGameStae.State = targetWinningState)
        |> Seq.map fst

    let loosingMoves = 
        legalMoves 
        |> Seq.map(fun move -> move, move |> playMoveWithToken game.Cells opponentToken)
        |> Seq.filter(fun (_ , nextGameStae) -> nextGameStae.State = loosingState)
        |> Seq.map fst
    
    match winningMoves, loosingMoves with
    | winningMoves, _ when winningMoves |> Seq.isEmpty |> not -> winningMoves |> Seq.head
    | _, loosingMoves when loosingMoves |> Seq.isEmpty |> not -> loosingMoves |> Seq.head
    | _ -> legalMoves |> Seq.head
