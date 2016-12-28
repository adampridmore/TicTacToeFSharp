#load "SeqUtils.fs"
#load "Tictactoe.fs"
#load "TictactoeComputerPlayer.fs"

open Tictactoe

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
        | OWon -> "Y"
        | Draw -> "D"
        | InProgress -> failwith "Invalid gate state"

    ([game.State |> gameStateToString] @ moveTextValues)
    |> Seq.reduce (sprintf "%s,%s")

let writeLinesToFile lineCount (lines: seq<string>) = 
    let filename = sprintf "tictactoe_1_%d.csv" lineCount
    let pathAndFilename = System.IO.Path.Combine(__SOURCE_DIRECTORY__, filename)
    
    let linesToWrite = 
        if not <| System.IO.File.Exists pathAndFilename then 
            Seq.concat [[ "Result,1,2,3,4,5,6,7,8,9"  ]|> List.toSeq;lines]
        else lines

    System.IO.File.AppendAllLines(pathAndFilename, linesToWrite)

let gamesToPlay = 1000000
seq{
    for _ in 1..gamesToPlay do
        yield playGame()
}
|> Seq.map (fun game -> game |> toCsvRow)
|> writeLinesToFile gamesToPlay


