#load "Tictactoe.fs"
open Tictactoe
    
newGame 
|> playMoveAndPrint (0,0) X 
|> playMoveAndPrint (1,1) O 
|> playMoveAndPrint (2,2) X 
|> ignore
