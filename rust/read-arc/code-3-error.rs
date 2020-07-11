use std::rc::Rc;
use std::thread;

fn main() {
    let rc = Rc::new(42);
    let thread = thread::spawn(move ||{
        eprintln!("value = {}", rc);
    });
    thread.join().unwrap();
}
