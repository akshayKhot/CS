using System;

namespace CC
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
                
                Print(parser.ParseTree);
            }
        }

        private static void Print(string tree)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(tree);

            Console.ForegroundColor = color;
        }
    }
}
