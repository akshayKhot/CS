using CC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class EvaluatorTest
    {
        [TestMethod]
        public void EvaluateTest()
        {
            string[] inputs = new string[] { "2 + 3", "5 - 3", "3 * 2", "10 / 2" };

            int[] expectedAnswers = new int[] { 5, 2, 6, 5 };

            for (int i = 0; i < inputs.Length; i++)
            {
                var parser = new Parser(inputs[i]);

                SyntaxTree tree = parser.Parse();

                var evaluator = new Evaluator(tree.Root);

                int result = evaluator.Evaluate();

                Assert.AreEqual(expectedAnswers[i], result);
            }
        }

        [TestMethod]
        public void EvaluatePrecedenceTest()
        {
            string[] inputs = new string[] { "1 + 2 * 3", "3 * 2 - 1 + 4" };

            int[] expectedAnswers = new int[] { 7, 9 };

            for (int i = 0; i < inputs.Length; i++)
            {
                var parser = new Parser(inputs[i]);

                SyntaxTree tree = parser.Parse();

                var evaluator = new Evaluator(tree.Root);

                int result = evaluator.Evaluate();

                Assert.AreEqual(expectedAnswers[i], result);
            }
        }
    }
}
