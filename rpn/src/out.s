.intel_syntax noprefix
.globl main
main:
  push 10
  push 3
  pop rdi
  pop rax
  mov rbx, rdi
  cdq
  idiv rbx
  push rax
  push 7
  push 5
  pop rdi
  pop rax
  imul rax, rdi
  push rax
  pop rdi
  pop rax
  add rax, rdi
  push rax
  push 1
  pop rdi
  pop rax
  sub rax, rdi
  push rax
  pop rax
  ret
