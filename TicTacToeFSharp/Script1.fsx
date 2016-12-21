#load "Tictactoe.fs"
#load "SeqUtils.fs"

open Tictactoe

let parseCharToInt32 (c:char) = 
    c.ToString() |> System.Int32.Parse

let readMove() = 
    let lineChar = System.Console.ReadLine().ToCharArray()
    let x = lineChar.[0] |> parseCharToInt32
    let y = lineChar.[2] |> parseCharToInt32
    x,y

let humanMoveAndPrint game = 
    game |> printGame
    let move = readMove()

    printfn "Move: %A" move

    playMove move game

let endGame = 
    Seq.unfoldUntil humanMoveAndPrint (fun g -> g.State <> GameState.InProgress) newGame
    |> Seq.last

printfn "Game Over"
printfn "Player %s won" (Some(endGame.NextMove |> tokenToggle) |> cellToString)
endGame |> printGame |> ignore

//newGame 
//////|> humanMoveAndPrint
//|> playMoveAndPrint (0,0)
//|> playMoveAndPrint (1,0)
//|> playMoveAndPrint (1,1)
//|> playMoveAndPrint (2,2)
//|> ignore

