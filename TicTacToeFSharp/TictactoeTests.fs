module TictactoeTests

open Tictactoe
open FsUnit
open NUnit.Framework


[<Test>]
let a() = 
    let rowWin = 
        [| 
            [| X; X; X |]
            [| Cell.Empty; Cell.Empty; Cell.Empty |]
            [| Cell.Empty; Cell.Empty; Cell.Empty |] 
        |]

    rowWin |> won |> should equal true


//
//let columnWin = 
//    [| 
//        [| Empty; Empty; O|]
//        [| Empty; Empty; O|]
//        [| Empty; Empty; O|] 
//    |]
//    
//let diagWin = 
//    [| 
//        [| Empty; Empty; O|]
//        [| Empty; O; Empty|]
//        [| O; Empty; Empty|] 
//    |]
//
//rowWin |> won
//columnWin |> won
//diagWin |> won
//newGame.Cells |> won

