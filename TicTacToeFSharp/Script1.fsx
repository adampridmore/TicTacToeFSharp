#load "SeqUtils.fs"
#load "Tictactoe.fs"

open Tictactoe

let getMove (game:Game) = 
    game |> getLegalMoves |> Seq.head

let endGame = playGame getMove
printfn "Game Over"
printfn "Player %s" (endGame.State |> gameStateToString)
endGame |> printGame 

//newGame 
//////|> humanMoveAndPrint
//|> playMoveAndPrint (0,0)
//|> playMoveAndPrint (1,0)
//|> playMoveAndPrint (1,1)
//|> playMoveAndPrint (2,2)
//|> ignore

