#load "SeqUtils.fs"
#load "Tictactoe.fs"
#load "TictactoeComputerPlayer.fs"
#load "FileHelpers.fsx"


#r @"..\packages\FSPowerPack.Parallel.Seq.Community.3.0.0.0\lib\Net40\FSharp.PowerPack.Parallel.Seq.dll"

open Tictactoe
open FileHelpers
open Microsoft.FSharp.Collections

let next game = 
    game 
    |> TictactoeComputerPlayer.getMove 
    |> playMove game

let isEnd (game:Game) = game.State <> GameState.InProgress

let playGame() = 
    newGame
    |> Seq.unfoldUntil next isEnd
    |> Seq.last

#time "on"

let toCsvRow (game: Game) =
    let moveTextValues = game.PreviousMoves |> Seq.map positionToNumber |> Seq.map string |> Seq.toList
    
    let gameStateToString = 
        function 
        | XWon -> "X"
        | OWon -> "O"
        | Draw -> "D"
        | InProgress -> failwith "Invalid gate state"

    ([game.State |> gameStateToString] @ moveTextValues)
    |> Seq.reduce (sprintf "%s,%s")

let writeLinesToFile (lines: seq<string>) = 
    let linesToWrite = 
        if not <| System.IO.File.Exists pathAndFilename then 
            Seq.concat [[ "Result,1,2,3,4,5,6,7,8,9"  ]|> List.toSeq;lines]
        else lines

    System.IO.File.AppendAllLines(pathAndFilename, linesToWrite)
    

let saveSingleGame =  toCsvRow >> Seq.singleton >> writeLinesToFile 

let gamesToPlay = 1000
seq{
    for _ in 1..gamesToPlay do
        yield playGame()
}
|> Seq.map saveSingleGame
|> Seq.last
//|> Seq.last |> ignore
//|> Seq.map (fun game -> game |> toCsvRow)
//|> writeLinesToFile



