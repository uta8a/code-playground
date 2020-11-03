# minilang
## やりたいこと
- F#の言語機能をフルに使って、言語処理系を作る。F#の機能を使うことが目標。
- あまり出力部分(codegen)の最適化は頑張らない、フロントエンド部分でF#の機能を使ったなにかをする
- sourceは自作言語、targetはwasm, wat
## Error
```
This expression was expected to have type    ''a -> 'b'    but here has type    'string'
Type mismatch. Expecting a    '(string -> Either<exn,string list>) -> 'a'    but given a    'Either<'b,'c> -> unit'    The type 'string -> Either<exn,string list>' does not match the type 'Either<'a,'b>' 
```
- 型ではまっている
- これはfunctionを使っているのに引数をやっていたのが原因
```
 The type 'exn' does not match the type 'obj []'
```
- Exceptionのエラー文取り出したい
```
Lookup on object of indeterminate type based on information prior to this program point. A type annotation may be needed prior to this program point to constrain the type of the object. This may allow the lookup to be resolved. 
```
- x.Message使って取り出そうとした
```
Type mismatch. Expecting a    'Either<exn,string list> -> 'a'    but given a    'Either<IoError,obj []> -> unit'    The type 'exn' does not match the type 'IoError'
```
- input |> evalで詰まっている。型AnnotationつけてOK
```fs
String.Format("Error: %A", e) // "Error: %A"
```
- これなに、％Aが出力されてしまうんだけど...エラーとか出ないのか→`{0}`で指定するパターンだった
- eでもいけるが、エラーメッセージがFilePathに関するものではない
- エラーメッセージを引き継ぐみたいなのできないのかな...
- assertもエラーメッセージ改善したい
- handleErrorで改善。エラーが足し算の形になっているときは、Error eをhandleError関数をつくってそこに渡して特定エラーのときにmatchさせるとよい。

## log
- 2020/11/03
  - すでにEither周りでコンピューテーション式とか出てくる。これは使いたい。
  - 例外処理周りでF#の言語機能が使えそうだなあ
  - Option/Resultは標準っぽい。Resultを使えばよさそうだけど、ここは敢えてEitherを定義して使ってみようか。
  - LeftがSomeっぽい気がするけど、Scalaに従いLeftをNoneにする。[ref. A Scala Either, Left, and Right example (like Option, Some, and None)](https://alvinalexander.com/scala/scala-either-left-right-example-option-some-none-null/)
  - 例外処理周りでEither使えたけど、メッセージの引き継ぎみたいなわからないところが多い。
  - https://github.com/fsprojects/awesome-fsharp awesomeを見つけた。例を見ていく
  - あまりよさげなawesomeではなかった
  - これErrorの扱い(ErrorChain的なこと)に関わりそう https://gist.github.com/mrange/1d2f3a26ca039588726fd3bd43cc8df3
  - https://stackoverflow.com/questions/43437412/use-bind-to-chain-continuous-functions
  - Resultのエラーかつエラーのとき合成っぽいことしてるな `@`
  - errorhandleでかなり進歩。
  - ファイル分割したらLib内で集約して、それをTestに持っていく。
  - ディレクトリ分割もまぁまぁ納得行く形になったのでOK
  - 各ディレクトリで `dotnet run` `dotnet test`すれば動く
