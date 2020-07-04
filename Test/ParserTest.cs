using CC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Resource;

namespace Test
{
    [TestClass]
    public class ParserTest
    {

        [TestMethod]
        public void ParseTest()
        {
            Dictionary<string, string> expressionTreeMap = StringResource.GetExpressionTreeMap();

            foreach (var expressionTree in expressionTreeMap)
            {
                var parser = new Parser(expressionTree.Key);

                string expectedTree = expressionTree.Value;
                string actualTree = parser.ParseTree;

                Assert.AreEqual(expectedTree, actualTree);
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
