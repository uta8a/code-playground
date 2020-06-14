from abc import ABCMeta, abstractmethod
from typing import TypeVar

MonoidType = TypeVar("MonoidType", bound="Monoid")

class Monoid(metaclass=ABCMeta):
    @abstractmethod
    def __mul__(self: MonoidType, x: MonoidType) -> MonoidType:
        pass

    @abstractmethod
    def identity(self: MonoidType) -> MonoidType:
        pass

    @abstractmethod
    def __eq__(self, x):
        pass

    def testAssociativity(self: MonoidType, a:MonoidType, b: MonoidType) -> bool:
        return (self * a) * b == self * (a * b)
    
    def testIdentity(self: MonoidType) -> bool:
        return self * self.identity() == self

GroupType = TypeVar("GroupType", bound="Group")

class Group(Monoid, metaclass=ABCMeta):
    @abstractmethod
    def inverse(self: GroupType) -> GroupType:
        pass

    def __truediv__(self: GroupType, x: GroupType) -> GroupType:
        return self * x.inverse()

    def testInverse(self: GroupType) -> bool:
        return self * self.inverse() == self.identity()

AdditiveGroupType = TypeVar("AdditiveGroupType", bound="AdditiveGroup")

class AdditiveGroup(metaclass=ABCMeta):

    @abstractmethod
    def __add__(self: AdditiveGroupType, x: AdditiveGroupType) -> AdditiveGroupType:
        pass
    @abstractmethod
    def zero(self: AdditiveGroupType) -> AdditiveGroupType:
        pass
    @abstractmethod
    def __neg__(self: AdditiveGroupType) -> AdditiveGroupType:
        pass
    def __sub__(self: AdditiveGroupType, x: AdditiveGroupType) -> AdditiveGroupType:
        return self + (-x)
    @abstractmethod
    def __eq__(self, x):
        pass

    def testAdditiveAssociativity(self: AdditiveGroupType, a: AdditiveGroupType, b: AdditiveGroupType) -> bool:
        return (self + a) + b == self + (a + b)
    def testZero(self: AdditiveGroupType) -> bool:
        return self + self.zero() == self
    def testNegative(self: AdditiveGroupType) -> bool:
        return self + (-self) == self.zero()
    def testAbelian(self: AdditiveGroupType, a: AdditiveGroupType) -> bool:
        return self  + a == a + self

RingType = TypeVar("RingType", bound="Ring")

class Ring(Monoid, AdditiveGroup):
    def testDistributive(self: RingType, a: RingType, b: RingType) -> bool:
        return self * (a + b) == self*a + self*b

class Integer(Ring):
    def __init__(self: "Integer", v: int) -> None:
        self.v = v
    def __repr__(self: "Integer") ->str:
        return str(self.v)
    def __mul__(self: "Integer", x: "Integer") -> "Integer":
        return Integer(self.v * x.v)
    def __add__(self: "Integer", x: "Integer") -> "Integer":
        return Integer(self.v + x.v)
    def __neg__(self: "Integer") -> "Integer":
        return Integer(-self.v)
    def __eq__(self, x):
        if not isinstance(x, Integer):
            return NotImplemented
        return self.v == x.v
    def identity(self: "Integer") -> "Integer":
        return Integer(1)
    def zero(self: "Integer") -> "Integer":
        return Integer(0)

Z = Integer # alias

