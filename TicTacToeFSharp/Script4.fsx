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
    //|> Seq.iter printGame

#time "on"

seq{
    for _ in 0..100000 do
        yield playGame()
}
//|> Seq.map (fun endGame -> endGame |> printGame; endGame)
|> Seq.groupBy (fun g -> g.State)
|> Seq.iter(fun (group, games) -> printfn "%s - %d" (group |> gameStateToString ) (games |> Seq.length) )
