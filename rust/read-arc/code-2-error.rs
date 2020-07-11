use std::rc::Rc;

fn main() {
    let mut rc = Rc::new(42);
    *rc = 53;
    eprintln!("rc = {}", rc);
}
