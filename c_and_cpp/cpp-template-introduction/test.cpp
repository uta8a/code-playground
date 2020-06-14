#include "add.h"

int main() {
  auto res = A::add(2, 4);  // 6
  std::cout << res << std::endl;
  res = A::add((float)2, (float)4);  // -2
  std::cout << res << std::endl;
  return 0;
}