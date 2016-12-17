module TictactoeTests

open Tictactoe
open FsUnit
open NUnit.Framework

[<Test>]
let ``row win``() = 
    [| 
        [| X; X; X |]
        [| Cell.Empty; Cell.Empty; Cell.Empty |]
        [| Cell.Empty; Cell.Empty; Cell.Empty |] 
    |]
    |> won |> should equal true

[<Test>]
let ``colum win``() = 
    [| 
        [| Cell.Empty; Cell.Empty; O|]
        [| Cell.Empty; Cell.Empty; O|]
        [| Cell.Empty; Cell.Empty; O|] 
    |]
    |> won |> should equal true

[<Test>]
let ``diagonal win``() = 
    [| 
        [| Cell.Empty; Cell.Empty; O|]
        [| Cell.Empty; O; Cell.Empty|]
        [| O; Cell.Empty; Cell.Empty|] 
    |]
    |> won |> should equal true

[<Test>]
let ``Empty board is not win``() = 
    emptyCells
    |> won |> should equal false

