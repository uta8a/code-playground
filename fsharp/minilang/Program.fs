// Learn more about F# at http://fsharp.org

open System

open System.IO

exception IoError of string

type Either<'E, 'U> =
    | Left of 'E
    | Right of 'U

let input =
    function
    // Return Either
    | filePath when not (File.Exists(filePath)) ->
        Left(IoError(String.Format("File path: {0} does not Exist.", filePath)))
    | filePath -> Right(Seq.toList (File.ReadLines(filePath)))

let eval (input) =
    let result =
        match input with
        | Left (e: exn) -> String.Format("Error: {0}", e)
        | Right (res: string list) -> String.Format("Result: {0}", res)

    result |> printfn "%A"

[<EntryPoint>]
let main argv =
    printfn "<<< Minilang Compiler"
    assert (argv.Length = 1)
    argv.[0] |> input |> eval
    argv.[0] |> printfn ">>> File Name: %A"
    0 // return an integer exit code
