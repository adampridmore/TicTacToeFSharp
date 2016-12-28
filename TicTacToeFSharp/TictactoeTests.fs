module TictactoeTests

open Tictactoe
open FsUnit
open NUnit.Framework

[<Test>]
let ``row win``() = 
    [| 
        [| Token(X); Token(X); Token(X) |]
        [| Cell.Empty; Cell.Empty; Cell.Empty |]
        [| Cell.Empty; Cell.Empty; Cell.Empty |] 
    |]
    |> won |> should equal true

[<Test>]
let ``colum win``() = 
    [| 
        [| Cell.Empty; Cell.Empty; Token(O)|]
        [| Cell.Empty; Cell.Empty; Token(O)|]
        [| Cell.Empty; Cell.Empty; Token(O)|] 
    |]
    |> won |> should equal true

[<Test>]
let ``diagonal win``() = 
    [| 
        [| Cell.Empty; Cell.Empty; Token(O)|]
        [| Cell.Empty; Token(O); Cell.Empty|]
        [| Token(O); Cell.Empty; Cell.Empty|] 
    |]
    |> won |> should equal true

[<Test>]
let ``Cell.Empty board is not win``() = 
    emptyCells
    |> won |> should equal false

[<Test>]
let ``Is not draw``() = 
    emptyCells |> isDraw |> should equal false
        
[<Test>]
let ``Is not draw 2``() = 
    [| 
        [| Cell.Empty; Cell.Empty; Token(O)|]
        [| Cell.Empty; Token(O); Cell.Empty|]
        [| Token(O); Cell.Empty; Cell.Empty|] 
    |]
    |> isDraw |> should equal false

[<Test>]
let ``Is draw ``() = 
    [| 
        [| Token(O);Token(O);Token(O)|]
        [| Token(O);Token(O);Token(O)|]
        [| Token(O);Token(O);Token(O)|] 
    |]
    |> isDraw |> should equal true

[<Test>]
let ``From position``()=
   1 |> numberToPosition |> should equal {X=0;Y=0}