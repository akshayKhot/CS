using System;
using System.Collections.Generic;

namespace cc
{
    class Parser
    {
        private int _position;

        private readonly SyntaxToken[] _tokens;

        private SyntaxToken Current => Peek(0);

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
        }

        public ExpressionSyntax Parse()
        {
            ExpressionSyntax left = ParsePrimaryExpression();

            while (Current.IsArithmetic())
            {
                SyntaxToken operatorToken = NextToken();
                ExpressionSyntax right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            SyntaxToken numberToken = Match(SyntaxKind.NumberToken);

            return new NumberExpressionSyntax(numberToken);
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            // why do we do this?
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private SyntaxToken Peek(int offset)
        {
            int index = _position + offset;

            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1]; // return last token

            return _tokens[index];
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }
    }
}
