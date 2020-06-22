// Learn more about F# at http://fsharp.org

open System
open FSharp.Scanf

[<EntryPoint>]
let main argv =
  printfn "What is the ultimate answer?"

  try
    let ans = scanfn "%i"
    if ans = 42 then
      printfn "correct."
    else
      printfn "%i? no." ans
  with
    | _ -> printfn "you entered something other than a number."
  0 // return an integer exit code
