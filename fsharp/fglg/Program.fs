// Learn more about F# at http://fsharp.org

open System

type Matrix<'T>(N: int) =
  let internalArray = Array2D.zeroCreate<'T> N N

  member this.Item
    with get (a: int, b: int) = internalArray.[a, b]
    and set (a: int, b: int) (value: 'T) = internalArray.[a, b] <- value

  override this.ToString() =
    let mutable output = "["
    for i = 0 to N - 1 do
      output <- output + "["
      for j = 0 to N - 1 do
        output <- output + String.Format("{0}, ", internalArray.[i, j])
      output <- output.[..output.Length - 3] + "],\n"
    output <- output.[..output.Length - 3] + "]"
    output

type FiniteGeneralLinearGroup(p: int, n: int) =
  let mutable A = Matrix<int>(n)

  let ext_gcd (a: int) (b: int) =
    let rec inner (r'', s'', t'') (r', s', t') =
      let step() =
        let q = r'' / r'
        let r = r'' - q * r'
        let s = s'' - q * s'
        let t = t'' - q * t'
        (r, s, t)
      if r' = 0 then (r'', s'', t'') else inner (r', s', t') (step())
    inner (a, 1, 0) (b, 0, 1)

  member this.inverse (d: int, p: int) =
    // d^(-1) mod p
    let (g: int, x: int, y: int) = ext_gcd d p
    assert (g = 1)
    (x + p) % p

  member this.p = p
  member this.n = n

  member this.Item
    with get (a: int, b: int) = A.[a, b]
    and set (a: int, b: int) (value: int) = A.[a, b] <- value

  override this.ToString() = A.ToString()

  member this.set_random() =
    let rnd = Random()
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        A.[i, j] <- (int (rnd.Next((int p))))

  member this.determinant() =
    if n = 1 then
      this.[0, 0]
    else
      let mutable det = 0
      for k = 0 to n - 1 do
        let X = new FiniteGeneralLinearGroup(p, n - 1)
        for i = 0 to n - 1 do
          if i = k then
            ()
          else
            for j = 1 to n - 1 do
              let ii =
                if i <= k then i else i - 1
              X.[ii, j - 1] <- A.[i, j]
        let sgn =
          let kk = k % 2
          match kk with
          | 0 -> 1
          | 1 -> -1
        det <- det + sgn * A.[k, 0] * (X.determinant())
      det

  member this.transpose() =
    let X = new FiniteGeneralLinearGroup(p, n)
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        X.[j, i] <- A.[i, j]
    X

  static member (+) (x: FiniteGeneralLinearGroup, y: FiniteGeneralLinearGroup) =
    assert (x.n = y.n)
    assert (x.p = y.p)
    let p = x.p
    let n = x.n
    let z = new FiniteGeneralLinearGroup(p, n)
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        z.[i, j] <- (x.[i, j] + y.[i, j] + p) % p
    z

  static member (-) (x: FiniteGeneralLinearGroup, y: FiniteGeneralLinearGroup) =
    assert (x.n = y.n)
    assert (x.p = y.p)
    let p = x.p
    let n = x.n
    let z = new FiniteGeneralLinearGroup(p, n)
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        z.[i, j] <- (x.[i, j] - y.[i, j] + p) % p
    z

  static member (~-) (x: FiniteGeneralLinearGroup) =
    let p = x.p
    let n = x.n
    let z = new FiniteGeneralLinearGroup(p, n)
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        z.[i, j] <- (-x.[i, j] + p) % p
    z

  static member (*) (x: FiniteGeneralLinearGroup, y: FiniteGeneralLinearGroup) =
    assert (x.p = y.p)
    assert (x.n = y.n)
    let p = x.p
    let n = x.n
    let z = new FiniteGeneralLinearGroup(p, n)
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        let mutable res = 0
        for k = 0 to n - 1 do
          res <- (res + x.[i, k] * y.[k, j]) % p
        z.[i, j] <- res
    z

  static member (*) (x: int, y: FiniteGeneralLinearGroup) =
    let p = y.p
    let n = y.n
    let z = new FiniteGeneralLinearGroup(p, n)
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        z.[i, j] <- (x * (y.[i, j])) % p
    z

  static member (*) (x: FiniteGeneralLinearGroup, y: int) =
    let p = x.p
    let n = x.n
    let z = new FiniteGeneralLinearGroup(p, n)
    for i = 0 to n - 1 do
      for j = 0 to n - 1 do
        z.[i, j] <- ((x.[i, j]) * y) % p
    z

  static member (.**) (x: FiniteGeneralLinearGroup, y: int) =
    let p = x.p
    let n = x.n
    let mutable X = new FiniteGeneralLinearGroup(p, n)
    match y with
    | y when y < 0 ->
        assert (x.determinant() % p <> 0)
        let mutable inv = new FiniteGeneralLinearGroup(p, n)
        for i = 0 to n - 1 do
          for j = 0 to n - 1 do
            let delta = new FiniteGeneralLinearGroup(p, n - 1)
            for k = 0 to n - 1 do
              if k = i then
                ()
              else
                for l = 0 to n - 1 do
                  if l = j then
                    ()
                  else
                    let ii =
                      if k <= i then k else k - 1

                    let jj =
                      if l <= j then l else l - 1

                    delta.[ii, jj] <- x.[k, l]
            let sgn =
              let sg = (i + j) % 2
              match sg with
              | 0 -> 1
              | 1 -> -1
            inv.[i, j] <- ((sgn * delta.determinant() % p + p) % p)
        inv <- inv.transpose()
        printfn "INV:\n%s" (inv.ToString())
        for i = 0 to n - 1 do
          for j = 0 to n - 1 do
            X.[i, j] <- if i = j then 1 else 0
        for i = 0 to -y - 1 do
          X <- X * inv
        X <- X * X.inverse (x.determinant(), p)
    | 0 ->
        for i = 0 to n - 1 do
          for j = 0 to n - 1 do
            X.[i, j] <- if i = j then 1 else 0
    | y when y > 0 ->
        for i = 0 to n - 1 do
          for j = 0 to n - 1 do
            X.[i, j] <- if i = j then 1 else 0
        for i = 0 to y - 1 do
          X <- X * x
    X

  override this.Equals(other) =
    match other with
      | :? FiniteGeneralLinearGroup as y ->
        assert (this.p = y.p)
        assert (this.n = y.n)
        let n = this.n
        let mutable res = true
        for i = 0 to n - 1 do
          for j = 0 to n - 1 do
            if this.[i, j] <> y.[i, j] then res <- false else res <- res
        res
      | _ -> false

[<EntryPoint>]
let main argv =
  let E = new FiniteGeneralLinearGroup(7, 3)
  E.set_random()
  let F = new FiniteGeneralLinearGroup(7, 3)
  F.set_random()
  printfn "E:\n%s" (E.ToString())
  printfn "F:\n%s" (F.ToString())
  printfn "det(E): %d" (E.determinant())
  printfn "E^T:\n%s" (E.transpose().ToString())
  printfn "E+F:\n%s" ((E + F).ToString())
  printfn "E-F:\n%s" ((E - F).ToString())
  printfn "-F:\n%s" ((-F).ToString())
  printfn "E*F:\n%s" ((E * F).ToString())
  printfn "3*E:\n%s" ((3 * E).ToString())
  printfn "E*3:\n%s" ((E * 3).ToString())
  printfn "E**-1:\n%s" ((E .** (-1)).ToString())
  printfn "E*E^-1:\n%s" ((E * (E .** (-1))).ToString())
  printfn "E**3:\n%s" ((E .** 3).ToString())
  printfn "E*E*E:\n%s" ((E * E * E).ToString())
  printfn "E**3 == E*E*E ? %A" ((E * E) = (E .** 2))
  0 // return an integer exit code
