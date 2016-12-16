module Tictactoe

type Cell = Empty | X | O

type Game = {
    Cells : Cell array array 
}

let newGame = 
    {
        Cells = Array.create 3 (Array.create 3 Empty)
    }

let gameToString (game:Game) =
    let cellToChar (c:Cell) = 
        match c with
        | Empty -> " "
        | X -> "X"
        | O -> "O"

    let rowToString (row: Cell array) = 
        row
        |> Seq.map cellToChar
        |> Seq.reduce (+)
    
    game.Cells 
    |> Seq.map rowToString
    |> String.concat "\r\n"

let printGame = gameToString >> (printfn "%s")

let playMove (x,y) (moveToPlay: Cell) (game:Game) =
    let isLegalMove = game.Cells.[y].[x] = Cell.Empty

    if not isLegalMove 
    then failwith (sprintf "Illegel move of '%A' at (%d, %d)\r\n%s"  moveToPlay x y (game |> gameToString) )
    else 
        let playMoveRow x (row : Cell array) = 
            row 
            |> Array.mapi (fun columnIndex cell -> if columnIndex = y then moveToPlay
                                                   else cell)

        let cells =     
            game.Cells 
            |> Array.mapi (fun rowIndex row -> if rowIndex = y then row |> playMoveRow x 
                                               else row)

        {
            Cells = cells
        }

let playMoveAndPrint (x,y) moveToPlay game = 
    printfn "Played %A at %d,%d" moveToPlay x y
    let next = playMove (x,y) moveToPlay game
    next |> printGame
    next


