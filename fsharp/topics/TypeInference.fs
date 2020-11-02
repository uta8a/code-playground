module TypeInference
// Int
let f a b  = a + b + 100
// uint (f_1 1u 2u) -> 3
let f_1 (x:uint32) y = 
    x + y
// uint
let f_2 x y =
    (x:uint32) + y
// uint
let f_3 x y : uint32 = 
    x + y 
// Int Only
let f_4 x y = 
    x + y
// Automatic Generalization
let f_auto a b = (a,b)