#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct Stack {
  int value;
  struct Stack *next;
};

struct Stack *push(int value, struct Stack *stack) {
  struct Stack *new_stack = malloc(sizeof(struct Stack));
  new_stack->value = value;
  new_stack->next = stack;
  return new_stack;
}

struct Stack *pop(struct Stack *stack) {
  return stack->next;
}

int peek(struct Stack *stack) { return stack->value; }

struct Stack *eval(char *tok, struct Stack *stack) {
  if (strcmp(tok, "+") == 0) {
    int y = peek(stack);
    stack = pop(stack);
    int x = peek(stack);
    stack = pop(stack);
    return push(x + y, stack);
  }
  if (strcmp(tok, "-") == 0) {
    int y = peek(stack);
    stack = pop(stack);
    int x = peek(stack);
    stack = pop(stack);
    return push(x - y, stack);
  }
  if (strcmp(tok, "*") == 0) {
    int y = peek(stack);
    stack = pop(stack);
    int x = peek(stack);
    stack = pop(stack);
    return push(x * y, stack);
  }
  if (strcmp(tok, "/") == 0) {
    int y = peek(stack);
    stack = pop(stack);
    int x = peek(stack);
    stack = pop(stack);
    return push(x / y, stack);
  }

  return push(atoi(tok), stack);
}
int main(void) {
  const int buf_max_len = 256;
  char buf[buf_max_len];

  char *source = calloc(buf_max_len, sizeof(char));

  while (fgets(buf, buf_max_len, stdin) != NULL) {
    source = realloc(source, (strlen(source) + strlen(buf) + 1) *
                                 sizeof(char)); // null char 1byte
    strcat(source, buf);
  }

  char *tok = strtok(source, " \t\r\n");
  if (tok == NULL) {
    return 1;
  }
  struct Stack *stack = eval(tok, NULL);

  while ((tok = strtok(NULL, " \t\r\n")) != NULL) {
    stack = eval(tok, stack);
  }
  printf("[");
  for (struct Stack *current = stack; current != NULL;
       current = current->next) {
    printf("%d", current->value);
    if (current->next) {
      printf(" ");
    } else {
      printf("]\n");
    }
  }
}
