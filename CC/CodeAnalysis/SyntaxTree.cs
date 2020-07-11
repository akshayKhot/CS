using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.CodeAnalysis
{
    public sealed class SyntaxTree
    {
        // Read-only, so the clients of SyntaxTree can't modify the errors
        public IReadOnlyList<string> Diagnostics { get; }

        public ExpressionSyntax Root { get; }
        
        public SyntaxToken EndOfFileToken { get; }

        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
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

        public override string ToString()
        {
            var builder = new StringBuilder();

            BuildTree(Root, builder);

            return builder.ToString();
        }
    }
}
