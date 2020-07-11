use std::rc::Rc;

fn main() {
    let x = Rc::new(42);

    assert_eq!(Rc::strong_count(&x), 1);

    let y = x.clone();

    assert_eq!(Rc::strong_count(&y), 2);

    assert!(Rc::ptr_eq(&x, &y));
    eprintln!("x = {0:p} (points to {0:})", x);
    eprintln!("y = {0:p} (points to {0:})", y);
}
