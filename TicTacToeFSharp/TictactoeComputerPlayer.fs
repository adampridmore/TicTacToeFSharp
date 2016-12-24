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
       
    let getMovesForTargetWinningState tokenToPlay targetState =
        legalMoves
        |> Seq.map(fun move -> move, move |> playMoveWithToken game.Cells Array.empty tokenToPlay)
        |> Seq.filter(fun (_ , nextGameStae) -> nextGameStae.State = targetState)
        |> Seq.map fst

    let winningMoves = getMovesForTargetWinningState myToken targetWinningState
    let loosingMoves = getMovesForTargetWinningState opponentToken loosingState
    
    match winningMoves |> Seq.any, loosingMoves |> Seq.any with
    | true, _ -> winningMoves |> Seq.head
    | _, true -> loosingMoves |> Seq.head
//    | _ -> legalMoves |> Seq.head
    | _ -> legalMoves |> Seq.random
