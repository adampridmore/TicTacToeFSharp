module TictactoeTests

open Tictactoe
open FsUnit
open NUnit.Framework

[<Test>]
let ``row win``() = 
    [| 
        [| Some(X); Some(X); Some(X) |]
        [| None; None; None |]
        [| None; None; None |] 
    |]
    |> won |> should equal true

[<Test>]
let ``colum win``() = 
    [| 
        [| None; None; Some(O)|]
        [| None; None; Some(O)|]
        [| None; None; Some(O)|] 
    |]
    |> won |> should equal true

[<Test>]
let ``diagonal win``() = 
    [| 
        [| None; None; Some(O)|]
        [| None; Some(O); None|]
        [| Some(O); None; None|] 
    |]
    |> won |> should equal true

[<Test>]
let ``Empty board is not win``() = 
    emptyCells
    |> won |> should equal false

[<Test>]
let ``Is not draw``() = 
    emptyCells |> isDraw |> should equal false
        
[<Test>]
let ``Is not draw 2``() = 
    [| 
        [| None; None; Some(O)|]
        [| None; Some(O); None|]
        [| Some(O); None; None|] 
    |]
    |> isDraw |> should equal false

[<Test>]
let ``Is draw ``() = 
    [| 
        [| Some(O);Some(O);Some(O)|]
        [| Some(O);Some(O);Some(O)|]
        [| Some(O);Some(O);Some(O)|] 
    |]
    |> isDraw |> should equal true