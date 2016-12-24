#r @"..\packages\Accord.3.3.0\lib\net45\Accord.dll"
#r @"..\packages\Accord.Neuro.3.3.0\lib\net45\Accord.Neuro.dll"
#r @"..\packages\Accord.Statistics.3.3.0\lib\net45\Accord.Statistics.dll"
#r @"..\packages\Accord.Math.3.3.0\lib\net45\Accord.Math.dll"
 
open Accord.Neuro.ActivationFunctions
open Accord.Neuro.Networks
open Accord.Neuro.Learning

let inputs = [|    
    [| 1.0;0.0 |] // class a
    [| 0.0;1.0 |] // class b
|]

let outputs = [|
    [| 1.0; 0.0 |];
    [| 0.0; 1.0 |];
|]

// Create a Bernoulli activation function
let ``function`` = new BernoulliFunction(alpha = 0.5);

// Create a Restricted Boltzmann Machine for 6 inputs and with 1 hidden neuron
let rbm = new RestrictedBoltzmannMachine(``function``, inputsCount= 2, hiddenNeurons= 2);

// Create the learning algorithm for RBMs
let teacher = new ContrastiveDivergenceLearning(rbm)
teacher.Momentum <- 0.0
teacher.LearningRate <- 0.1
teacher.Decay <- 0.0

// learn 5000 iterations
seq{0..5000}
|> Seq.iter (fun _ -> teacher.RunEpoch(inputs) |> ignore )

// Compute the machine answers for the given inputs:
let a = rbm.Compute([|1.0; 1.0|]); // { 0.99, 0.00 }
let b = rbm.Compute([|0.0; 0.0|]); // { 0.00, 0.99 }

// As we can see, the first neuron responds to vectors belonging
// to the first class, firing 0.99 when we feed vectors which 
// have 1s at the beginning. Likewise, the second neuron fires 
// when the vector belongs to the second class.

// We can also generate input vectors given the classes:
let xa = rbm.GenerateInput([|1.0; 0.0|]); // { 1, 1, 1, 0, 0, 0 }
let xb = rbm.GenerateInput([|0.0; 1.0|]); // { 0, 0, 1, 1, 1, 0 }

// As we can see, if we feed an output pattern where the first neuron
// is firing and the second isn't, the network generates an example of
// a vector belonging to the first class. The same goes for the second
// neuron and the second class.