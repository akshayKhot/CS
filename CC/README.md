# This is a compiler written in C-Sharp. 

### What does it do right now?

Not much, yet. It parses the expression into a syntax tree and dumps the tree, along with the result on the screen. 

For example, 


    > 3 + 5 + 8

    └──BinaryExpression
        ├──BinaryExpression
        │   ├──NumberExpression
        │   │   └──NumberToken 3
        │   ├──PlusToken
        │   └──NumberExpression
        │       └──NumberToken 5
        ├──PlusToken
        └──NumberExpression
            └──NumberToken 8

    16 (1 ms)

### What will it do when it grows up?

Write small scripts, execute and evaluate expressions. Support functions, especially first-class functions that you can pass around. 
