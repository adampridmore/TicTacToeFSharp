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

    playMove move game

let endGame = 
    Seq.unfold (fun state -> 
        let nextState = state |> humanMoveAndPrint; 
        Some(nextState,nextState) ) newGame
    // TODO - This doesn't include the last game state...
    |> Seq.takeWhile (fun game -> game.State = GameState.InProgress)
    |> Seq.last

printfn "Game Over"
printfn "Player %s won" (Some(endGame.NextMove |> tokenToggle) |> cellToString)
endGame |> printGame 

//newGame 
//////|> humanMoveAndPrint
//|> playMoveAndPrint (0,0)
//|> playMoveAndPrint (1,0)
//|> playMoveAndPrint (1,1)
//|> playMoveAndPrint (2,2)
//|> ignore

