using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC
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

        public string ParseTree
        {
            get
            {
                var expression = Parse();

                var builder = new StringBuilder();

                BuildTree(expression, builder);

                return builder.ToString();
            }
        }

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

        // Gets the token at the offset position 
        // from the current position. 
        public SyntaxToken Peek(int offset)
        {
            int index = _position + offset;

            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1]; // return last token

            return _tokens[index];
        }

        #region Private Methods

        private ExpressionSyntax ParsePrimaryExpression()
        {
            SyntaxToken numberToken = Match(SyntaxKind.NumberToken);

            return new NumberExpressionSyntax(numberToken);
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diagnostics.Add($"ERROR(Parse): Unexpected token <{Current.Kind}>, expected <{kind}>");

            // why do we do this?
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private static void BuildTree(SyntaxNode node, StringBuilder builder, string indent = "", bool isLast = true)
        {
            string marker = isLast ? "└──" : "├──";
            builder.Append(indent);
            builder.Append(marker);
            builder.Append(node.Kind);

            if (node is SyntaxToken token && token.Value != null)
            {
                builder.Append(" ");
                builder.Append(token.Value);
            }

            builder.AppendLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (SyntaxNode child in node.GetChildren())
            {
                BuildTree(child, builder, indent, child == lastChild);
            }
        }
        
        #endregion
    }
}
