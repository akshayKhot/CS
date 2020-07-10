using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Resource
{
    public static class StringResource
    {
        public static string FirstTree =>
@"└──BinaryExpression
    ├──BinaryExpression
    │   ├──NumberExpression
    │   │   └──NumberToken 1
    │   ├──PlusToken
    │   └──NumberExpression
    │       └──NumberToken 2
    ├──MinusToken
    └──NumberExpression
        └──NumberToken 3
";

        public static string SecondTree =>
@"└──BinaryExpression
    ├──NumberExpression
    │   └──NumberToken 2
    ├──StarToken
    └──NumberExpression
        └──NumberToken 3
";

        public static string ThirdTree =>
@"└──NumberExpression
    └──NumberToken 1
";

        public static string FourthTree =>
@"└──BinaryExpression
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
";

        public static Dictionary<string, string> GetExpressionTreeMap()
        {
            var expressionTree = new Dictionary<string, string>()
            {
                { "1 + 2 - 3", FirstTree },
                { "2 * 3", SecondTree },
                { "1", ThirdTree },
                { "3 * (7 - (1 + 4))", FourthTree }
            };

            return expressionTree; 
        }
    }
}
