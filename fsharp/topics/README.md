# Topics
- kodaiさんのツイートを見てF#を言語の観点から触ってみたいと思った(ありがとうkodaiさん)
- 調べるのは以下のトピック
```
型推論 -> ok
判別共用体 -> ok
パターンマッチング -> ok
アクティブパターン
コンピュテーション式
型プロバイダ
SRTP(静的解決される型パラメータ)
```

## やってみたlog
- fsharpでファイル単体で実行する方法がわからないので、Program.fsで各トピックを実行して、各トピックをファイル分割する。
- なんと言語リファレンスに型推論と判別共用体があるじゃないか。これ見てみよう
- dotnet3.1でやっている
- takにたずねてみた(5.0previewにすると嬉しいこと、バージョンを上げる理由)
```
tak: 明確な切り替わりは僕も覚えていないんですが、個人的に1番違うのはfsxのスクリプトファイルにnugetパッケージを直接書けるようになったことですかね。スクリプトとしてのF#の地位がかなり向上しました！他にもdotnet newのデフォルトをF#にしたり、単純なパフォーマンスが向上したり…イロイロ
```
- nugetがわからない。dotnet new(これはプロジェクト作るときのやつやな)

## ファイル分割
- moduleとnamespaceの2つの概念があるらしい。
- [Modules ドキュメント](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/modules)
- moduleとは値、型、関数値のまとまりを指す。名前衝突を避けるためにある。
- moduleを書かなくても、コンパイルすると`module Program`のようなものが入る
- ひとつのファイルで複数moduleを使える
- モジュールの中身を使うときは、`ModuleName.Identifier`か`open ModuleName`する。個人的にはopenしちゃうとせっかくの名前衝突を避けたいのが台無し感あるのでここは`ModuleName.Identifier`でいく。特にimport keywordはいらないのかな？
- [Namespaces ドキュメント](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/namespaces)
- モジュールをまとめることのできる強い概念っぽい。
- とりあえずmoduleを使おう。`M.fs`と`Program.fs`でやってみる
- だいぶ苦労した。以下のようなエラーが出る
```
$ dotnet run
/home/uta8a/project/workspace/code-playground/fsharp/topics/Program.fs(8,13): error FS0039: The value, namespace, type or module 'M' is not defined. [/home/uta8a/project/workspace/code-playground/fsharp/topics/topics.fsproj]

The build failed. Fix the build errors and run again.
```
- え？勝手にファイル認識してくれないんだ。調べてけっきょく以下のように手動でfsprojにM.fsを足して解決。
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Compile Include="M.fs" />
    <Compile Include="Program.fs" />
  </ItemGroup>

</Project>
```
- 流石にこれは...。プロジェクト単位での実行時に手動で依存関係にあるもの加えるの面倒すぎる。(あと、Includeの順番も大事らしい。マジか...)
- 今回はmoduleを使うことにする。

## 型推論
- [Type Inference ドキュメント](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/type-inference)
- [language specification](https://fsharp.org/specs/language-spec/)
- これ見て、3.1は古いからpreviewでも5.0を使うべきではという気持ちになった。
- 引数、または返り値から推論を行いエラーを出してくれる
- Automatic Generaliztion 自動ジェネリック化とは、パラメータの型に依存しない関数をジェネリックと言って、型を指定しない場合に自動的にジェネリックにしてくれるケースがあるらしい？
```fsharp
// Int Only
let f_nonauto x y = 
    x + y
// Automatic Generalization
let f_auto a b = (a,b)
```
- 上の例のように、足し算は`+`演算子がデフォルトでInt型を引数にとるのでジェネリックではない(例えば `f_nonauto 1u 2u` でエラー)が、下の`f_auto`はジェネリックになる
- では足し算ジェネリックを実現するには？→inline keywordをつけて静的に解決される型パラメータをつかうらしい
- これってSRTPのことを指しているのだろうか？あとでSRTPを調べるときに思い出したい。
- 参考: https://www.slideshare.net/Gab_km/feinline-andtypeinferrenceinf-sharp

## 判別共用体
- [Discriminated Unions ドキュメント](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions)
- 調べてみるとTypeScriptにもDiscriminated Unionがあるらしい(https://typescript-jp.gitbook.io/deep-dive/type-system/discriminated-unions)
- 以下のように使う
```fs
type Shape =
    | Rectangle of width : float * length : float
    | Circle of radius : float
    | Prism of width : float * float * height : float
```
- これだけ見ると、別に
```fs
type Rectangle = float * float
```
- のように個別に名付けていけばいいような気もする。
- しかし！判別共用体は再帰できる。名前をつけてそれを定義の中で利用できるとなると、名付けた意味が出てくる
```fs
type Expression =
    | Number of int
    | Add of Expression * Expression
    | Multiply of Expression * Expression
    | Variable of string

let rec Evaluate (env:Map<string,int>) exp =
    match exp with
    | Number n -> n
    | Add (x, y) -> Evaluate env x + Evaluate env y
    | Multiply (x, y) -> Evaluate env x * Evaluate env y
    | Variable id    -> env.[id]

let environment = Map.ofList [ "a", 1 ;
                               "b", 2 ;
                               "c", 3 ]

// Create an expression tree that represents
// the expression: a + 2 * b.
let expressionTree1 = Add(Variable "a", Multiply(Number 2, Variable "b"))

// Evaluate the expression a + 2 * b, given the
// table of values for the variables.
let result = Evaluate environment expressionTree1 // 5
```
- これは面白い。
- あと、メンバ関数を設定できる。classみたいな雰囲気あるけど、ORの構造をとっている時点で別物だしなんだろうこれは。初めて食べる食べ物的な感覚になった。

## パターンマッチング
- [Pattern Matching ドキュメント](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/pattern-matching)
- ドキュメントを見ると、いろいろなパターンに応じてマッチさせることができるようだ。
- Constant Patternは馴染みがある。他に、Rustで見かけるOptionっぽいのがIdentifier patternにある。
```fs
let printOption (data : int option) =
    match data with
    | Some var1  -> printfn "%d" var1
    | None -> ()
```
- 以下でエラー出た。いい感じにできないかな
```fs
let printOption (data) =
    match data with
    | Some var1  -> var1
    | None -> "None Value"
printOption(Some(10)) |> printfn "%A"
// This expression was expected to have type    'string'    but here has type    'int'
```
- StringとIntの判別共用体を返すとすればいいのか？
```fs
let printOption (data) =
    match data with
    | Some var1  -> string var1
    | None -> "None Value"
// This expression was expected to have type    'obj'    but here has type    'int'
```
- string castをしてもだめだった。dataにAnnotationがひつようらしい。
- 以下で通る
```fs
let printOption (data : int option) =
    match data with
    | Some var1  -> string var1
    | None -> "None Value"
```
- 型推論難しい...
- Type Test Patternとか面白そう。継承が絡むclassについて、ぴったりその型か、もしくは継承元に一致したときにマッチする。

## アクティブパターン

## コンピューテーション式

## 型プロバイダ

## SRTP(静的解決される型パラメータ)

## Question
- そもそもF#のドキュメントはどれ見ればいいの
  - MSのドキュメント？
  - https://docs.microsoft.com/ja-jp/dotnet/fsharp/
  - https://docs.microsoft.com/en-us/dotnet/fsharp/
  - 英語版の方がFilter By Titleで検索しやすい
- Code Formatterは？
  - https://github.com/fsprojects/fantomas を使っている。dotnet経由でインストール