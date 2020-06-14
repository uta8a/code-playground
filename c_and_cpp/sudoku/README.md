# SUDOKU
- ref. https://www.geeksforgeeks.org/sudoku-backtracking-7/
- Input: `grid[N][N]` N = 9
- Output: `printGrid: fn grid[N][N] -> ()`
- solveSudoku: solver

# 仕様
- 9x9のマスに数独を書く
- タテヨコのそれぞれ9マスを見たときに123456789がそろっている状態を出力する
- subgrid 3x3に1-9が入っているか調べる

# Verify
- タテヨコに1-9があるか調べる
- subgrid 3x3に1-9が入っているか調べる

# cycle
```
clang-format-9 -i main.c
gcc -o main main.c
./main
```
# 感想
- わざわざ再帰のたびにすべてのマス目を探索していたのがよくなかった。再帰のときはrow, column, subgrid1個のみを探索してvalidかどうか調べればよい。

# ref.
- https://github.com/sandmark/py-sudoku/blob/develop/sudoku.py のソースコードをverify時に参考にしました
