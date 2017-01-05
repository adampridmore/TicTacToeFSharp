#r @"..\packages\Accord.Neuro.3.3.0\lib\net45\Accord.Neuro.dll"
#r @"..\packages\Accord.Statistics.3.3.0\lib\net45\Accord.Statistics.dll"
#r @"..\packages\Accord.Math.3.3.0\lib\net45\Accord.Math.dll"
#r @"..\packages\Accord.3.3.0\lib\net45\Accord.dll"

open Accord.Neuro
open Accord.Neuro.Learning

//Accord.Neuro.Learning.BackPropagationLearning

// http://accord-framework.net/docs/html/T_Accord_Neuro_Learning_BackPropagationLearning.htm


// initialize input and output values
let input = [| 
    [|0.0;0.0|]
    [|0.0;1.0|]
    [|1.0;0.0|]
    [|1.0;1.0|]
|]
let output = [|
    [|0.0|]
    [|1.0|]
    [|1.0|]
    [|0.0|]
|]

let network = 
    new ActivationNetwork(
        SigmoidFunction(2.0),
        2, // two inputs in the network
        2, // two neurons in the first layer
        1 
    ); // one neuron in the second layer

// create teacher
let teacher = new BackPropagationLearning( network );

// loop
Seq.initInfinite id
|> Seq.map (fun _ -> teacher.RunEpoch(input, output))
|> Seq.takeWhile (fun error -> error > 0.1)
|> Seq.truncate 100
|> Seq.last

network.Compute([|1.0;1.0|])
