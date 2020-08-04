# RPNで電卓を作る を読んで電卓を作る
- ref. 
- 1.echoするプログラム
- これは簡単
- 2.連結
- シーアロック、リアロック(calloc, realloc)
- strcat(ストアキャット) s1の領域は長めにとっておく必要がある
- 3.空白区切りのトークンに分割
- sep separator
- strtok
- 4.stack
- stackの実装
- 5.stackに整数を積む
- atoi(えーとぅあい) 
- 関数化
- `-Wall`
- 6.RPN電卓
- 辞書順比較
- 演算子の実装
- x.終わりに
- sqrtの実装、普通の電卓にする

# 追加でやってみる
- 中間言語として、アセンブリに変換してみる
- RISC-Vでやってみるとか？
- 言語処理関係ないけどまあええか
- RISC-V
```
git clone --recursive <gnu-toolchain>
./configure --prefix=/usr/local/bin/
make linux
```
- gnu-toolchainの導入。これで`riscv64-unknown-elf-*`が使えるようになる
- めっちゃクローンに時間かかる
- configureは時間かからないけど、makeには時間がかかる RV64GC 向けでやってみた
- RISC-Vでいきなりは難しそう。まずはx86\_64でやってみる？


# x86\_64.c
- stackに値が残ったままだとセグフォする
- `idiv`には`cdq`が大事。後で調べておく
