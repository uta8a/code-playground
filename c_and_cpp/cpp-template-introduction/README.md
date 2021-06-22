# C++ template を学ぶ
- https://qiita.com/hal1437/items/b6deb22a88c76eeaf90c
  - 優しい感じだったのでこれをした。
- `clang-format -i *.h *.cpp`
- `g++ -o main test.cpp`
- **挙動**
```
T add(T a, U b){}
add(2, 4.6) // 6
```
- Tにキャストされるので、切り捨てが起こっている
- **特殊化**
- float floatで変えることにした。これパズルになりそう(型と型の間で変なことができるので)
```cpp
auto res = A::add(2, 4);  // 6
std::cout << res << std::endl;
res = A::add((float)2, (float)4);  // -2
std::cout << res << std::endl;
```
- 関数template
- class template
- member template
- alias template