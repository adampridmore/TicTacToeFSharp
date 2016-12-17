module Tictactoe

type Token = X | O
type Cell = Token Option

type GameState = InProgress | XWon | OWon

type Game = {
    Cells : Cell array array
    State : GameState
    NextMove : Token
}

let emptyCells = Array.create 3 (Array.create 3 None)

let tokenToggle = function | X -> O | O -> X

let newGame = 
    {
        Cells = emptyCells
        State = InProgress
        NextMove = X
    }

let cellToString (c:Cell) = 
    match c with
    | None -> " "
    | Some(X) -> "X"
    | Some(O) -> "O"

let gameToString (game:Game) =
    let rowToString (row: Cell array) = 
        row
        |> Seq.map cellToString
        |> Seq.reduce (+)
    
    let stateText = 
        function 
        | InProgress -> "In Progress"
        | XWon -> "X Won"
        | OWon-> "O Won"

    [game.State |> stateText ] @ (game.Cells |> Seq.map rowToString |> Seq.toList )
    |> String.concat "\r\n"

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
        |> Seq.where ( fun (a,_, _) -> a <> None)
        |> Seq.exists (fun (a,b,c) -> a = b && a = c)

    Seq.concat [ rowsPicker;columnsPicker;diagPicker ] |> areThreeCellsWin

let printGame = gameToString >> (printfn "%s")

let playMove (x,y) (game:Game) =
    let isLegalMove = game.Cells.[y].[x] = None

    if not isLegalMove 
    then failwith (sprintf "Illegel move of '%A' at (%d, %d)\r\n%s"  game.NextMove x y (game |> gameToString) )
    else 
        let playMoveRow (row : Cell array) : (Cell array) = 
            row 
            |> Array.mapi (fun columnIndex cell -> if columnIndex = x then Some(game.NextMove)
                                                   else cell )

        let cells =     
            game.Cells 
            |> Array.mapi (fun rowIndex row -> if rowIndex = y then row |> playMoveRow
                                               else row)

        let state = 
            match (cells |> won) with 
            | true -> match game.NextMove with 
                      | X -> XWon
                      | Y -> OWon
            | false -> GameState.InProgress
        
        {
            Cells = cells
            State = state
            NextMove = game.NextMove |> tokenToggle
        }

let playMoveAndPrint (x,y) game = 
    printfn "Played %A at %d,%d" game.NextMove x y
    let next = playMove (x,y) game
    next |> printGame
    next