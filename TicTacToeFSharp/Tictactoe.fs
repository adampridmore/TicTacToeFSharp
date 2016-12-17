module Tictactoe

type Cell = Empty | X | O

type GameState = InProgress | XWon | OWon

type Game = {
    Cells : Cell array array;
    State : GameState 
}

let newGame = 
    {
        Cells = Array.create 3 (Array.create 3 Empty);
        State = InProgress
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
    
    let stateText = 
        function 
        | InProgress -> "In Progress"
        | XWon -> "X Won"
        | OWon-> "O Won"

    [game.State |> stateText ] @ (game.Cells |> Seq.map rowToString |> Seq.toList )
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
            Cells = cells;
            State = InProgress
        }

let playMoveAndPrint (x,y) moveToPlay game = 
    printfn "Played %A at %d,%d" moveToPlay x y
    let next = playMove (x,y) moveToPlay game
    next |> printGame
    next

let won (cells: Cell array array) = 
    let rowsPicker = 
        seq{0..2} |> Seq.map (fun i -> cells.[0].[i],cells.[1].[i],cells.[2].[i])
        
    let columnsPicker = 
        seq{0..2} |> Seq.map (fun i -> cells.[i].[0],cells.[i].[1],cells.[i].[2])
    
    let diagPicker = 
        seq{
            yield (cells.[0].[0], cells.[1].[1], cells.[2].[2])
            yield (cells.[0].[2], cells.[1].[1], cells.[2].[0])
        }

    let areThreeCellsWin (cells : seq<Cell*Cell*Cell>) = 
        cells
        |> Seq.where (fun (a,_, _) -> a <> Empty)
        |> Seq.exists (fun (a,b,c) -> a = b && a = c)

    Seq.concat [ rowsPicker;columnsPicker;diagPicker ] |> areThreeCellsWin



