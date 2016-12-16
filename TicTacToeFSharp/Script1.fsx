type Cell = Empty | X | O
type Game = {
    Cells : Cell array array 
}

let emptyGame = 
    {
        Cells = Array.create 3 (Array.create 3 Empty)
    }
    
let playMove (x,y) (move: Cell) (game:Game) =
    let playMoveRow x (row : Cell array) = 
        row 
        |> Array.mapi (fun columnIndex cell -> if columnIndex = y then move else cell)

    let cells =     
        game.Cells 
        |> Array.mapi (fun rowIndex row -> match rowIndex with 
                                           | rowIndex when rowIndex = y -> game.Cells.[rowIndex] |> playMoveRow x 
                                           | _ -> game.Cells.[rowIndex])

    {
        Cells = cells
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
    |> Seq.reduce (sprintf "%s\r\n%s")

let printGame = gameToString >> (printfn "%s")

//emptyGame |> gameToString |> 

emptyGame 
|> playMove (0,0) X 
|> playMove (1,1) O 
|> playMove (2,2) X 
|> printGame

