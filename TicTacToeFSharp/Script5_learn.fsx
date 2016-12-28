#load "SeqUtils.fs"
#load "Tictactoe.fs"
#load "TictactoeComputerPlayer.fs"
#load "FileHelpers.fsx"

open Tictactoe
open FileHelpers

let rowToGame (line:string) = 
    let values = 
        line.Split([|','|], System.StringSplitOptions.RemoveEmptyEntries)
        |> Seq.toArray

    let gameState = 
        match values.[0] with
        | "X" -> XWon
        | "O" -> OWon
        | "D" -> Draw
        | x -> failwith (sprintf "Unknown gamestate: %A" x)

    let previousMoves = 
        values 
        |> Seq.skip 1
        |> Seq.map (fun (s:string) -> s |> int32)
        |> Seq.map numberToPosition
        |> Seq.toArray

                
    {
        Game.State = gameState;
        Game.Cells = emptyCells;
        Game.PreviousMoves = previousMoves;
        NextMove = X        
    }

let loadGames =
    if not (System.IO.File.Exists(pathAndFilename)) then failwith (sprintf "Filenot found %s" pathAndFilename)
    else 
        System.IO.File.ReadAllLines(pathAndFilename)
        |> Seq.skip 1
        |> Seq.map rowToGame

loadGames 
|> Seq.head 
|> printGame