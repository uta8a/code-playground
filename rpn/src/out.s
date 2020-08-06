.intel_syntax noprefix
.globl main
main:
  push 10
  push 3
  pop rdi
  pop rax
  mov rbx, rdi
  cqo
  idiv rbx
  push rax
  pop rax
  ret
