module PatternMatching
// Identifier Pattern
let printOption (data : int option) =
    match data with
    | Some var1  -> string var1
    | None -> "None Value"