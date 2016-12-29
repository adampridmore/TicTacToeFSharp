﻿#load "SeqUtils.fs"
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

    Seq.fold playMove newGame previousMoves

let loadGames =
    if not (System.IO.File.Exists(pathAndFilename)) then failwith (sprintf "Filenot found %s" pathAndFilename)
    else 
        System.IO.File.ReadAllLines(pathAndFilename)
        |> Seq.skip 1
        |> Seq.map rowToGame

//loadGames 
////|> Seq.iter printGame
//|> Seq.map (fun game -> (game.State,game.PreviousMoves.[0] |> positionToNumber) )
//|> Seq.groupBy id
//|> Seq.map (fun (g, games) -> (g, games |> Seq.length) )
//|> Seq.sortBy (fun g -> g |> fst |> snd)
//|> Seq.iter (printfn "%A")

let playGameAndLearn (game: Game) =
    let output = 
        match (game.State) with
        | XWon -> 1.0 
        | OWon -> -1.0 
        | Draw -> 0.0
        | _ -> failwith "Invalid state"

    let gameMoves = 
        Seq.mapFold (fun game move -> (game, (playMove game move) )  ) newGame (game.PreviousMoves)
        |> fst
        |> Seq.filter (fun game -> game.NextMove = Token.X)

    let gameToInputs (game:Game) =
        let cellToInputValue (positiveValue:Cell) (c:Cell) = 
            match c with
            | x when x = positiveValue -> 1.0
            | _ -> 0.0

        let inputIndexToValue i =
            match i with
            | o when o < 9 -> 
                let p = (o+1 |> numberToPosition)
                game.Cells.[p.Y].[p.X] |> (cellToInputValue Cell.Empty)
            | o when o >= 9 && o < 18 -> 
                let p = (o+1-9 |> numberToPosition)
                game.Cells.[p.Y].[p.X] |> (cellToInputValue (Cell.Token(Token.O) ) )
            | o -> 
                let p = (o+1-18 |> numberToPosition)
                game.Cells.[p.Y].[p.X] |> (cellToInputValue (Cell.Token(Token.X) ) )

        seq{0..(3*9-1)}
        |> Seq.map inputIndexToValue

    let learn (output: float) (inputs: seq<float>) = 
        // Pass to ML engine.
        ()

    gameMoves 
    |> Seq.map gameToInputs 
    |> Seq.iter (learn output)

loadGames |> Seq.head |> playGameAndLearn

