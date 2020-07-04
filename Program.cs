using System;
using System.Linq;

namespace cc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                Console.Write("> "); 

                var line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line) || line == "clear")
                    return;

                var parser = new Parser(line);

                ExpressionSyntax expression = parser.Parse();

                Print(expression);
            }
        }

        private static void Print(ExpressionSyntax expression)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            PrettyPrint(expression);

            Console.ForegroundColor = color;
        }

        /*
        ├── include
        │   ├── foo
        │   └── bar
        */
        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            string marker = isLast ? "└──" : "├──";
            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken token && token.Value != null)
            {
                Console.Write(" ");
                Console.Write(token.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (SyntaxNode child in node.GetChildren())
            {
                PrettyPrint(child, indent, child == lastChild);
            }
        }
    }
}
