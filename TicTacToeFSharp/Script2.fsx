#load "SeqUtils.fs"


//let takeUntil fn (items: seq<'T>) = 
//    items
//    |> Seq.map (fun i -> i, fn i)
//    |> Seq.pairwise
//    |> Seq.takeWhile (fun (_,(_,b)) -> b)
//    |> Seq.map (fst >> fst)
//
//seq{0..10}
//|> Seq.takeWhile (fun x -> x <= 5)
//|> Seq.iter (printfn "%d")
//
//seq{0..10}
//|> takeUntil (fun x -> x <= 5)
//|> Seq.iter (printfn "%d")
//
//
//
//
//Seq.unfold (fun (state, more) -> 
//    if (more) then 
//        let nextState = state + 1
//        if state <= 5 then Some(state, (nextState, true))
//        else Some(state, (nextState, false))
//    else None
//    ) (0, true)
//|> Seq.iter (printfn "%d")
//
//let next state = state + 1
//let isLastItem x = x <= 5
//Seq.unfold (fun (state, more) -> 
//    let lastItem = state |> isLastItem 
//    match (more) with
//    | true -> Some(state, (state |> next, lastItem) )
//    | false -> None
//) (0, true)
//|> Seq.iter (printfn "%d")

//module Seq
//
//let unfoldUntil nextState isLastItem initialState = 
//    Seq.unfold (fun (state, noMore) -> 
//        let lastItem = state |> isLastItem 
//        match (noMore) with
//        | true -> Some(state, (state |> nextState, not lastItem) )
//        | false -> None
//    ) (initialState, true)
//
//unfoldUntil (fun x -> printfn "a - %d" (x + 1) ;  x + 1) (fun x -> x= 6) 0 
////|> Seq.iter (printfn "%d")
////|> Seq.iter (fun _ -> () )
//|> Seq.last




//let initialState = 0
//let next x = (printfn "N%d" (x + 1) ) ; x + 1
//let isLastItem x = x = 6
//
//Seq.unfold (fun ( (state: Option<int>) ,(lastItem: bool)) -> 
//    match state,lastItem with
//    | None,_ -> None
//    | Some(x), true -> Some(x, (None, false))
//    | Some(x), false ->
//                    let nextState = x |> next
//                    let lastItem = (nextState |> isLastItem)
//                    Some(x, (Some(nextState), lastItem) )
//) (Some(initialState), false)
//|> Seq.map(fun x -> (printfn "%d" x);x)
//|> Seq.last

let unfoldUntil next isLastItem initialState = 
    Seq.unfold (fun ( (state: Option<int>) ,(lastItem: bool)) -> 
        match state,lastItem with
        | None,_ -> None
        | Some(x), true -> Some(x, (None, false))
        | Some(x), false ->
                        let nextState = x |> next
                        let lastItem = (nextState |> isLastItem)
                        Some(x, (Some(nextState), lastItem) )
    ) (Some(initialState), false)

let initialState = 0
let next x = (printfn "N%d" (x + 1) ) ; x + 1
let isLastItem x = x = 6
unfoldUntil next isLastItem initialState
//|> Seq.map(fun x -> (printfn "%d" x);x)
|> Seq.last
|> (printfn "Last: %d")



// I wonder if you can use Unfold?