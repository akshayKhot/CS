using CC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class ParserTest
    {

        [TestMethod]
        public void ParseTest()
        {
            var parsers = new Parser[]
            {
                new Parser("1 + 2 - 3"),
                new Parser("2 * 3"),
                new Parser("1")
            };

            var expectedTrees = new string[]
            {
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
",
@"└──BinaryExpression
    ├──NumberExpression
    │   └──NumberToken 2
    ├──StarToken
    └──NumberExpression
        └──NumberToken 3
",
@"└──NumberExpression
    └──NumberToken 1
"
            };

            for (int i = 0; i < parsers.Length; i++)
            {
                string expected = expectedTrees[i];
                string actual = parsers[i].ParseTree;

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CurrentTest()
        {
            var parser = new Parser("1 + 2 - 3");

            var expected = new SyntaxToken(SyntaxKind.NumberToken, 0, "1", 1);
            Assert.AreEqual(expected, parser.Current);
        }

        [TestMethod]
        public void PeekTest()
        {
            var line = "1 + 2 - 3";

            var expectedTokens = new SyntaxToken[]
           {
                new SyntaxToken(SyntaxKind.NumberToken, 0, "1", 1),
                new SyntaxToken(SyntaxKind.PlusToken, 2, "+", null),
                new SyntaxToken(SyntaxKind.NumberToken, 4, "2", 2),
                new SyntaxToken(SyntaxKind.MinusToken, 6, "-", null),
                new SyntaxToken(SyntaxKind.NumberToken, 8, "3", 3),
                new SyntaxToken(SyntaxKind.EndOfFileToken, 9, "\0", null)
           };

            var parser = new Parser(line);

            for (int i = 0; i < expectedTokens.Length; i++)
            {
                var expected = expectedTokens[i];
                var actual = parser.Peek(i);

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
