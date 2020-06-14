fn dot(v: &Vec<f64>, u: &Vec<f64>) -> f64 {
    if v.len() != u.len() {
        panic!("Error dot: Vec Length is different.");
    }
    let mut res: f64 = 0.0;
    for i in 0..v.len() {
        res += v[i] * u[i];
    }
    res
}
fn norm(v: &Vec<f64>) -> f64 {
    dot(v, v).sqrt()
}
fn div(v: &Vec<f64>, m: f64) -> Vec<f64> {
    let mut res: Vec<f64> = vec![];
    for i in 0..v.len() {
        res.push(v[i] / m);
    }
    res
}
fn scale(v: &Vec<f64>, m: f64) -> Vec<f64> {
    let mut res: Vec<f64> = vec![];
    for i in 0..v.len() {
        res.push(v[i] * m);
    }
    res
}
fn minus(v: &Vec<f64>, u: &Vec<f64>) -> Vec<f64> {
    if v.len() != u.len() {
        panic!("Error minus: Vec Length is different.");
    }
    let mut res: Vec<f64> = vec![];
    for i in 0..v.len() {
        res.push(v[i] - u[i]);
    }
    res
}
fn normalize(v: &Vec<f64>) -> Vec<f64> {
    div(&v, norm(&v))
}
fn main() {
    // origin basis v
    let v_1: Vec<f64> = vec![1.0, 2.0, -1.0];
    let v_2: Vec<f64> = vec![-1.0, 3.0, 1.0];
    let v_3: Vec<f64> = vec![4.0, 0.0, -1.0];

    let u_1 = normalize(&v_1);
    println!("{:?}", u_1);

    let _v_2 = minus(&v_2, &scale(&u_1, dot(&v_2, &u_1)));
    let u_2 = normalize(&_v_2);
    println!("{:?}", u_2);

    let _v_3 = minus(
        &minus(&v_3, &scale(&u_1, dot(&v_3, &u_1))),
        &scale(&u_2, dot(&v_3, &u_2)),
    );
    let u_3 = normalize(&_v_3);
    println!("{:?}", u_3);
    // ans: orthonomal basis u
}
