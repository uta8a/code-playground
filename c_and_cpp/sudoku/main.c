#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>

#define N 9

bool inputCheck(int grid[N][N]) {
  for (int i = 0; i < N; i++) {
    for (int j = 0; j < N; j++) {
      if (grid[i][j] < 0 || grid[i][j] > 9) {
        return false;
      }
    }
  }
  return true;
}

bool filled(int grid[N][N]) {
  for (int i = 0; i < N; i++) {
    for (int j = 0; j < N; j++) {
      if (grid[i][j] < 1 || grid[i][j] > 9) {
        return false;
      }
    }
  }
  return true;
}
bool usedInC(int grid[N][N], int c, int target) {
  for (int r = 0; r < N; r++) {
    if (grid[c][r] == target) {
      return true;
    }
  }
  return false;
}
bool usedInR(int grid[N][N], int r, int target) {
  for (int c = 0; c < N; c++) {
    if (grid[c][r] == target) {
      return true;
    }
  }
  return false;
}
bool usedInS(int grid[N][N], int sc, int sr, int target) {
  for (int c = 0; c < 3; c++) {
    for (int r = 0; r < 3; r++) {
      if (grid[sc + c][sr + r] == target) {
        return true;
      }
    }
  }
  return false;
}
bool solveSudoku(int grid[N][N], int depth) {
  if (depth > 81) {
    printf("log: Wrong.Depth > 81.\n");
    exit(1);
  }
  // filled?
  if (filled(grid)) {
    printf("log: complete\n");
    return true;
  }
  // c: column, r: row
  int c = depth / N;
  int r = depth % N;
  if (grid[c][r] != 0) {
    if (solveSudoku(grid, depth + 1)) {
      return true;
    }
  } else {
    // i: number
    for (int i = 1; i <= N; i++) {
      if (!usedInC(grid, c, i) && !usedInR(grid, r, i) &&
          !usedInS(grid, c - c % 3, r - r % 3, i)) {
        grid[c][r] = i;
        if (solveSudoku(grid, depth + 1)) {
          return true;
        }
        grid[c][r] = 0;
      }
    }
  }
  return false;
}

void printGrid(int grid[N][N]) {
  for (int i = 0; i < N; i++) {
    for (int j = 0; j < N; j++) {
      printf("%2d", grid[i][j]);
    }
    printf("\n");
  }
}

int main(void) {
  // 0 is unfilled
  int grid[N][N] = {{3, 0, 6, 5, 0, 8, 4, 0, 0}, {5, 2, 0, 0, 0, 0, 0, 0, 0},
                    {0, 8, 7, 0, 0, 0, 0, 3, 1}, {0, 0, 3, 0, 1, 0, 0, 8, 0},
                    {9, 0, 0, 8, 6, 3, 0, 0, 5}, {0, 5, 0, 0, 9, 0, 6, 0, 0},
                    {1, 3, 0, 0, 0, 0, 2, 5, 0}, {0, 0, 0, 0, 0, 0, 0, 7, 4},
                    {0, 0, 5, 2, 0, 6, 3, 0, 0}};
  // int grid[N][N] = {{0}};
  if (inputCheck(grid) == false) {
    printf("InputError: Invalid Number.[0-9] is fine.\n");
    exit(1);
  }
  if (solveSudoku(grid, 0)) {
    printGrid(grid);
  } else {
    printf("No Solution.\n");
  }
  return 0;
}
