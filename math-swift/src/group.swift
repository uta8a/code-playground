protocol Group: Equatable {
    static func *(a: Self, b: Self) -> Self
    static var identity: Self {get}
    var inverse: Self {get}
}
extension Group {
    static func testAssociativity(a: Self, _ b: Self, _ c: Self) -> Bool {
        return (a * b) * c == a * (b * c)
    }

    static func testIdentity(a: Self) -> Bool {
        return (a * Self.identity == a) && (Self.identity * a == a)
    }

    static func testInverse(a: Self) -> Bool {
        return (a * a.inverse == Self.identity) && (a.inverse * a == Self.identity)
    }
}