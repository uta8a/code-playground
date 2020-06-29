# #0 uchan C++講習会
- [youtube](https://www.youtube.com/watch?v=pLuZOUusBYI)

## Install
- clang [Arch Linuxでのパッケージ](https://www.archlinux.org/packages/extra/x86_64/clang/)
```
❯ clang --version
clang version 10.0.0
Target: x86_64-pc-linux-gnu
Thread model: posix
InstalledDir: /usr/bin
```

## Command
```
clang++ main.cpp

```
## Note
- C++98, C+03, C++11あたりが有名なバージョン
- :star: includeってなんですか？
  - ファイルの中身をとってきてそこに展開する
- backslashと円マーク 文字コードの問題
- errorメッセージの読み方
- :star: `#include "iostream"`でも動く？
  - どこのファイルを探しに行くかが異なる。`"iostream"`はまず同じフォルダ内の`iostream`というファイルがないか探す。`<iostream>`はシステム標準の場所を探す。
  - 検証してみた
  - 同じディレクトリ内にiostreamという名前のファイルがあるときの挙動が異なる
- objdumpもテキストじゃないか -> xxdとかでみたやつを見やすくしてくれている
- `std::`についての説明
- `cout` (しーあうと) streamに流し込まれる
- 文字列リテラル そのもののデータが埋め込まれている
- `int main` プログラムが起動するところ。mainが一番最初に起動する場所となっている
- statementは`;`で終わる
- シフト演算子がstreamで使えるのは、言語が提供している機能だから。
- `return 0` C++ではmain関数だけはreturn省略できる
- コマンドの復習
  - vim
  - clang++ `-o`
  - objdump `-C`: マングリングされているものに対し有効
  - less: /word, shift+nで上方向に検索
  - fg
  - jobs
  - readelf
