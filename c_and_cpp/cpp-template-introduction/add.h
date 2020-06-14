#include <iostream>

namespace A {
  template <typename T, typename U>
  T add(T a, U b) {
    return a + b;
  }
  template <>
  float add(float a, float b) {
    return a - b;
  }
}