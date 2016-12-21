module SeqUtilTests

open FsUnit
open NUnit.Framework

[<Test>]
let ``Sequence until``() = 
    let next x = x + 1;
    let isLastItem x = x = 5 
    Seq.unfoldUntil next isLastItem 0
    |> Seq.toList 
    |> should equal [|0;1;2;3;4;5|]
