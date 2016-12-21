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
