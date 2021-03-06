﻿module Tictactoe

type Token = X | O

type Cell = 
    | Token of Token
    | Empty

type GameState = InProgress | Draw | XWon | OWon

type Game = {
    Cells : Cell array array
    State : GameState
    NextMove : Token
}

let emptyCells = Array.create 3 (Array.create 3 Empty)

let tokenToggle = function | X -> O | O -> X

let newGame = 
    {
        Cells = emptyCells
        State = InProgress
        NextMove = X
    }

let gameStateToString = 
    function
    | InProgress -> "In Progress"
    | Draw -> "Draw"
    | XWon -> "X Won"
    | OWon -> "O Won"

let tokenToString = 
    function 
    | X -> "X" 
    | O -> "O"

let cellToString = 
    function
    | Empty-> " "
    | Token(t) -> t |> tokenToString


let private cellsToString (cells: Cell array array) = 
    let rowToString (row: Cell array) = 
        row
        |> Seq.map cellToString
        |> Seq.reduce (+)
    
    cells 
    |> Seq.map rowToString 
    |> Seq.toList 
    |> String.concat "\r\n"

let private gameToString (game:Game) =
    let stateText = 
        function 
        | InProgress -> sprintf "%s to play" (game.NextMove |> tokenToString)
        | state ->  state |> gameStateToString

    [game.State |> stateText ] @ [ game.Cells |> cellsToString ]
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
        |> Seq.where ( fun (a,_, _) -> a <> Empty)
        |> Seq.exists (fun (a,b,c) -> a = b && a = c)

    Seq.concat [ rowsPicker;columnsPicker;diagPicker ] |> areThreeCellsWin

let isDraw (cells: Cell array array) = 
    cells
    |> Seq.exists (fun row -> row |> Seq.exists (fun c -> c = Empty))
    |> not
    
let printGame = gameToString >> (printfn "%s")

let private isInProgress (game:Game) = game.State <> InProgress

let playMoveWithToken (cells:Cell array array) (token:Token) (x,y) =
    let isLegalMove = cells.[y].[x] = Empty

    if not isLegalMove 
    then failwith (sprintf "Illegel move of '%A' at (%d, %d)\r\n%s"  token x y (cells |> cellsToString) )
    else 
        let playMoveRow (row : Cell array) : (Cell array) = 
            row 
            |> Array.mapi (fun columnIndex cell -> if columnIndex = x then Token(token)
                                                   else cell )

        let newCells =     
            cells 
            |> Array.mapi (fun rowIndex row -> if rowIndex = y then row |> playMoveRow
                                               else row)
                                                       
        let state = 
            match (newCells |> won) with 
            | true -> match token with 
                      | X -> XWon
                      | Y -> OWon
            | false ->  match newCells |> isDraw with
                        | true -> GameState.Draw
                        | false -> GameState.InProgress
        
        {
            Cells = newCells
            State = state
            NextMove = token |> tokenToggle
        }

let playMove (game:Game) (move : int*int) = 
    playMoveWithToken game.Cells game.NextMove move

let playGame (makeMove: Game -> (int*int)) = 
    let nextMove game = 
        game 
        |> makeMove 
        |> playMove game

    Seq.unfoldUntil nextMove isInProgress newGame 
    |> Seq.last


let getLegalMoves (game:Game) = 
    seq{
        for y in 0..2 do 
            for x in 0..2 do 
                yield (x,y)
        }
    |> Seq.filter (fun (x,y) -> game.Cells.[y].[x] = Empty) 
