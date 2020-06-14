from algebra_py import __version__, Monoid, Z


def test_version():
    assert __version__ == '0.1.0'

class A(object):
    def __init__(self, data):
        self.data = data
    def __mul__(self, x):
        return A(self.data * x.data)

def test_A_mul():
    a = A(5)
    b = A(10)
    assert (a*b).data == 50

def test_z():
    a = Z(10)
    b = Z(22)
    assert a.identity() == Z(1)
    assert a.zero() == Z(0)
    assert a + b == Z(32)
    assert a * b == Z(220)
