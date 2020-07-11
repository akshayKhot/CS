using CC;
using CC.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Test
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void CurrentTest()
        {
            var lexers = new Lexer[]
            {
                new Lexer("A"),
                new Lexer("a"),
                new Lexer(""),
                new Lexer("Hello World"),
                new Lexer("1"),
                new Lexer("32 + 2 * 3"),
                new Lexer("  ")
            };

            var expectedCurrents = new char[] { 'A', 'a', '\0', 'H', '1', '3', ' ' };

            for (int i = 0; i < lexers.Length; i++)
            {
                Assert.AreEqual(expectedCurrents[i], lexers[i].Current);
            }
        }

        [TestMethod]
        public void NextTest()
        {
            var lexers = new Lexer[]
            {
                new Lexer("A"),
                new Lexer("a"),
                new Lexer(""),
                new Lexer("Hello World"),
                new Lexer("1"),
                new Lexer("32 + 2 * 3"),
                new Lexer("  ")
            };

            var expectedCurrents = new char[] { '\0', '\0', '\0', 'e', '\0', '2', ' ' };

            for (int i = 0; i < lexers.Length; i++)
            {
                lexers[i].Next();
                Assert.AreEqual(expectedCurrents[i], lexers[i].Current);
            }
        }

        [TestMethod]
        public void NextTokenTest()
        {
            string line = "1 + 2 - 3";
            
            var lexer = new Lexer(line);

            var expectedTokens = new SyntaxToken[]
            {
                new SyntaxToken(SyntaxKind.NumberToken, 0, "1", 1),
                new SyntaxToken(SyntaxKind.WhitespaceToken, 1, " ", null),
                new SyntaxToken(SyntaxKind.PlusToken, 2, "+", null),
                new SyntaxToken(SyntaxKind.WhitespaceToken, 3, " ", null),
                new SyntaxToken(SyntaxKind.NumberToken, 4, "2", 2),
                new SyntaxToken(SyntaxKind.WhitespaceToken, 5, " ", null),
                new SyntaxToken(SyntaxKind.MinusToken, 6, "-", null),
                new SyntaxToken(SyntaxKind.WhitespaceToken, 7, " ", null),
                new SyntaxToken(SyntaxKind.NumberToken, 8, "3", 3),
            };

            foreach (SyntaxToken expectedToken in expectedTokens)
            {
                SyntaxToken actualToken = lexer.NextToken();
                Assert.AreEqual(expectedToken, actualToken);
            }
        }

        [TestMethod]
        public void ErrorTest()
        {
            string line = "H";

            var lexer = new Lexer(line);

            var token = lexer.NextToken();

            Assert.AreEqual(1, lexer.Diagnostics.Count());
            Assert.AreEqual("ERROR(LEX): bad character input: 'H'", lexer.Diagnostics.First());
        }
    }
}
