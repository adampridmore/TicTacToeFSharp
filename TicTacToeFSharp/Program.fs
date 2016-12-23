module program

open Tictactoe

let parseCharToInt32 (c:char) = 
    c.ToString() |> System.Int32.Parse

[<EntryPoint>]
let main argv = 
    let readMove() = 
        let lineChar = System.Console.ReadLine().ToCharArray()
        let x = lineChar.[0] |> parseCharToInt32
        let y = lineChar.[2] |> parseCharToInt32
        x,y

    let humanMoveAndPrint game = 
        game |> printGame
        let move = readMove()
        printfn "Move: %A" move
        move 
        
    let endGame = playGame humanMoveAndPrint

    printfn "Game Over"
    printfn "Player %s" (endGame.State |> gameStateToString)
    endGame |> printGame 

    0 // return an integer exit code
