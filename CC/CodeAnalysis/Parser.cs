using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.CodeAnalysis
{
    public class Parser
    {
        #region Fields

        private int _position;

        private readonly SyntaxToken[] _tokens;

        private List<string> _diagnostics = new List<string>();

        #endregion

        public SyntaxToken Current => Peek(0);

        public IEnumerable<string> Diagnostics => _diagnostics;

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            
            SyntaxToken token;

            do
            {
                token = lexer.NextToken();

                if (token.IsValid())
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EndOfFileToken);

            _tokens = tokens.ToArray();

            // Don't forget what the lexer recorded.
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public SyntaxTree Parse()
        {
            ExpressionSyntax expression =  ParseTerm();

            SyntaxToken endOfFileToken = Match(SyntaxKind.EndOfFileToken);

            var tree = new SyntaxTree(_diagnostics, expression, endOfFileToken);

            return tree;
        }

        // A term is just a bunch of factors that are strung together by '+' or '-'
        private ExpressionSyntax ParseTerm()
        {
            ExpressionSyntax leftFactor = ParseFactor();

            while (Current.IsPlusMinus())
            {
                SyntaxToken operatorToken = NextToken();
             
                ExpressionSyntax rightFactor = ParseFactor();
                
                leftFactor = new BinaryExpressionSyntax(leftFactor, operatorToken, rightFactor);
            }

            return leftFactor;
        }

        private ExpressionSyntax ParseFactor()
        {
            ExpressionSyntax left = ParsePrimaryExpression();

            while (Current.IsFactor())
            {
                SyntaxToken operatorToken = NextToken();
                
                ExpressionSyntax right = ParsePrimaryExpression();
                
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        #region Private Methods

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                SyntaxToken leftParenthesis = NextToken();

                ExpressionSyntax expression = ParseExpression();
                
                SyntaxToken rightParenthesis = Match(SyntaxKind.CloseParenthesisToken);

                return new ParenthesizedExpressionSyntax(leftParenthesis, expression, rightParenthesis);
            }

            SyntaxToken numberToken = Match(SyntaxKind.NumberToken);

            return new NumberExpressionSyntax(numberToken);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diagnostics.Add($"ERROR(Parse): Unexpected token <{Current.Kind}>, expected <{kind}>");

            // We make our token after reporting the error, if the expected token is not present. 
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        // Gets the token at the offset position 
        // from the current position. 
        public SyntaxToken Peek(int offset)
        {
            int index = _position + offset;

            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1]; // return last token

            return _tokens[index];
        }

        #endregion
    }
}
