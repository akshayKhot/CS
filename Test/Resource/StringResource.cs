using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Resource
{
    public static class StringResource
    {
        public static string First =>
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

        public static string Second =>
@"└──BinaryExpression
    ├──NumberExpression
    │   └──NumberToken 2
    ├──StarToken
    └──NumberExpression
        └──NumberToken 3
";

        public static string Third =>
@"└──NumberExpression
    └──NumberToken 1
";
    }
}
