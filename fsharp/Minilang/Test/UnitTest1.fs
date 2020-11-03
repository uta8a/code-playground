module Test

open NUnit.Framework
open Minilang.Main

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =
    Assert.Pass()

[<Test>]
let ``test 2 is ok?`` () =
    Assert.AreEqual ("hello" |> input |> eval, null )