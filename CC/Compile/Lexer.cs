using System.Collections.Generic;

namespace CC
{
    public class Lexer
    {
        private readonly string _text;

        private int _position;

        private List<string> _diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => _diagnostics; 

        public char Current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';

                return _text[_position];
            }
        }

        public Lexer(string text)
        {
            _text = text;
            _position = 0;
        }

        public SyntaxToken NextToken()
        {
            if (_position >= _text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            }

            if (char.IsDigit(Current))
            {
                int start = _position;

                while (char.IsDigit(Current))
                    Next();

                int length = _position - start;
                string text = _text.Substring(start, length);
                int.TryParse(text, out int value);
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                int start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();

                int length = _position - start;
                string text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (Current == '+')
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);

            if (Current == '-')
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);

            if (Current == '*')
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);

            if (Current == '/')
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);

            if (Current == '(')
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);

            if (Current == ')')
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);

            _diagnostics.Add($"ERROR(LEX): bad character input: '{Current}'");

            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }

        public void Next()
        {
            _position++;
        }
    }
}
