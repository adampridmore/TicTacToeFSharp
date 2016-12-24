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

let writeLinesToFile (lines: seq<string>) = 
    let filename = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "tictactoe_1.csv")
    
    let linesToWrite = 
        if not <| System.IO.File.Exists filename then 
            Seq.concat [[ "Result,1,2,3,4,5,6,7,8,9"  ]|> List.toSeq;lines]
        else lines

    System.IO.File.AppendAllLines(filename, linesToWrite)

seq{
    for _ in 1..1000 do
        yield playGame()
}
|> Seq.map (fun game -> game |> toCsvRow)
|> writeLinesToFile


