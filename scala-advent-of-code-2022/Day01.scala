@main def main: Unit = {
  for(line <- io.Source.stdin.getLines()) {
    println(line)
  }
}