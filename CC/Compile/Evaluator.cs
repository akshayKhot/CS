using System;

namespace CC
{
    public class Evaluator
    {
        private ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax expression)
        {
            if (expression is NumberExpressionSyntax numberExp)
                return (int)numberExp.NumberToken.Value;

            if (expression is BinaryExpressionSyntax binExp)
            {
                int left = EvaluateExpression(binExp.Left);
                int right = EvaluateExpression(binExp.Right);

                int result = Calculate(left, right, binExp.OperatorToken.Kind);

                return result;
            }

            if (expression is ParenthesizedExpressionSyntax parenthesizedExpression)
            {
                return EvaluateExpression(parenthesizedExpression.Expression);
            }

            throw new Exception($"Unexpected expression node: {expression.Kind}");
        }

        private int Calculate(int left, int right, SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                    return left + right;

                case SyntaxKind.MinusToken:
                    return left - right;

                case SyntaxKind.StarToken:
                    return left * right;

                case SyntaxKind.SlashToken:
                    return left / right;

                default:
                    throw new Exception($"Unexpected binary operator: {kind}");
            }

        }
    }
}