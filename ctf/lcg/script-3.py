"""
input

m
x[0] x[1] x[2]
"""
# ref: http://anh.cs.luc.edu/331/code/xgcd.py
def xgcd(a, b):
    x0, x1, y0, y1 = 1, 0, 0, 1
    while b:
        q, r = a // b, a % b
        x1, x0 = x0 - q * x1, x1
        y1, y0 = y0 - q * y1, y1
        a, b = b, r
    return a, x0, y0


m = int(input())
x_0, x_1, x_2 = map(int, input().split())

_, inv, _ = xgcd(x_1 - x_0, m)
a = (x_2 - x_1) * inv % m
b = (x_1 - a * x_0) % m
x_next = (a * x_2 + b) % m

print("x[3] = ", x_next)
"""
input

573530770110344462788799394512078393
29059535932284908898780570780835250 445562389898029848807854085740374544 402636745678392148494612813091348649
"""
