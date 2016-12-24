module Seq

let unfoldUntil next isLastItem initialState = 
    Seq.unfold (fun ( (state: Option<'T>) ,(lastItem: bool)) -> 
        match state,lastItem with
        | None,_ -> None
        | Some(x), true -> Some(x, (None, false))
        | Some(x), false ->
                        let nextState = x |> next
                        let lastItem = (nextState |> isLastItem)
                        Some(x, (Some(nextState), lastItem) )
    ) (Some(initialState), false)


let any s = s |> Seq.isEmpty |> not

let private rnd = new System.Random();

let random (s:seq<'T>) : ('T) = 
    let length = s |> Seq.length
    let indexToTake = rnd.Next(length)

    s |> Seq.item indexToTake
