// Learn more about F# at http://fsharp.org
module A = TypeInference
module B = DiscriminatedUnions
module C = PatternMatching

open System

[<EntryPoint>]
let main argv =
    // Modules
    // add file name to topics.fsproj
    let x = M.x
    printfn "Hello World from F#! %d" x
    // Type Inference
    printfn "Type Inference %d %d %d" (A.f 10 20) (A.f_1 1u 2u) (A.f_4 1 5)
    A.f_auto 2 1 |> printfn "tuple %A" // Automatic Generalization
    A.f_auto 2u 1 |> printfn "tuple %A"
    // Discriminated Unions
    B.Rectangle( 1., 10.) |> printfn "%A" // type
    B.Evaluate B.environment B.expressionTree1 |> printfn "Evaluate Result: %A" // 5
    // Pattern Matching
    C.printOption(Some(10)) |> printfn "%A"

    0 // return an integer exit code
