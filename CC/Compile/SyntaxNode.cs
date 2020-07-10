using System.Collections.Generic;

namespace CC
{
    // 1 + 2 * 3
    //
    //		+
    //	  /   \
    //	1 		*
    //		  /   \
    //		2		3
    //
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }

    public abstract class ExpressionSyntax : SyntaxNode
    {
    }

    public sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken NumberToken { get; }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }

    public sealed class BinaryExpressionSyntax : ExpressionSyntax
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

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }

    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.ParenthsizedExpression;

        public SyntaxToken LeftParenthesis { get; }

        public ExpressionSyntax Expression { get; }
        
        public SyntaxToken RightParenthesis { get; }

        public ParenthesizedExpressionSyntax(SyntaxToken leftParenthesis, ExpressionSyntax expression, SyntaxToken rightParenthesis)
        {
            LeftParenthesis = leftParenthesis;
            Expression = expression;
            RightParenthesis = rightParenthesis;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LeftParenthesis;
            yield return Expression;
            yield return RightParenthesis;
        }
    }
}
