# This is a compiler written in C-Sharp. 

### What does it do right now?

Not much, yet. It parses the expression into a syntax tree and dumps the tree, along with the result on the screen. 

For example, 

    > 3 * (7 - (1 + 4))

    └──BinaryExpression
    ├──NumberExpression
    │   └──NumberToken 3
    ├──StarToken
    └──ParenthsizedExpression
        ├──OpenParenthesisToken
        ├──BinaryExpression
        │   ├──NumberExpression
        │   │   └──NumberToken 7
        │   ├──MinusToken
        │   └──ParenthsizedExpression
        │       ├──OpenParenthesisToken
        │       ├──BinaryExpression
        │       │   ├──NumberExpression
        │       │   │   └──NumberToken 1
        │       │   ├──PlusToken
        │       │   └──NumberExpression
        │       │       └──NumberToken 4
        │       └──CloseParenthesisToken
        └──CloseParenthesisToken

    6 (1 ms)

### What will it do when it grows up?

Write small scripts, execute and evaluate expressions. One of my ambitious goal is to support functions, especially first-class functions. 

### Object Diagram 
![uml diagram](https://github.com/akshayKhot/CS/blob/master/CC/Meta/uml.png)
