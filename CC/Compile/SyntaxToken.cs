using System;
using System.Collections.Generic;
using System.Linq;

namespace CC
{
    class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override SyntaxKind Kind { get; }

        public int Position { get; }

        public string Text { get; }

        public object Value { get; }

        internal bool IsValid()
        {
            return Kind != SyntaxKind.WhitespaceToken &&
                    Kind != SyntaxKind.BadToken;
        }

        // As tokens are leaf nodes, they don't have any children
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        public bool IsArithmetic()
        {
            return Kind == SyntaxKind.PlusToken ||
                    Kind == SyntaxKind.MinusToken ||
                    Kind == SyntaxKind.StarToken ||
                    Kind == SyntaxKind.SlashToken;
        }
    }
}
