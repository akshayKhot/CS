namespace cc
{
    // 1 + 2 * 3
    //
    //		+
    //	  /   \
    //	1 		*
    //		  /   \
    //		2		3
    //
    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }
    }

    abstract class ExpressionSyntax : SyntaxNode
    {
    }

    sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken NumberToken { get; }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }
    }

    sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public ExpressionSyntax Left { get; }

        public SyntaxToken OperatorToken { get; }

        public ExpressionSyntax Right { get; }

        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }
    }
}
