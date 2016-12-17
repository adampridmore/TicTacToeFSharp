#load "Tictactoe.fs"

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

    playMove move Token.X game

Seq.unfold (fun state -> 
    let nextState = state |> humanMoveAndPrint; 
    Some(nextState,nextState) ) newGame
|> Seq.takeWhile (fun game -> game.State = GameState.InProgress)
|> Seq.iter (ignore)


//newGame 
////|> humanMoveAndPrint
//|> playMoveAndPrint (0,0) X 
//|> playMoveAndPrint (1,1) O 
//|> playMoveAndPrint (2,2) X 
//|> ignore

