using BenchmarkDotNet.Running;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

                Console.WriteLine();

                var clock = Stopwatch.StartNew();

                if (string.IsNullOrWhiteSpace(line) || line == "clear")
                    return;

                var parser = new Parser(line);

                var syntaxTree = parser.Parse();

                Print(syntaxTree);

                Print(clock);
            }
        }

        private static void Print(SyntaxTree syntaxTree)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(syntaxTree);

            Console.ForegroundColor = color;

            PrintErrorsIfAny(syntaxTree.Diagnostics);
        }

        private static void PrintErrorsIfAny(IEnumerable<string> errors)
        {
            if (errors.Any())
            {
                var color = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Red;

                foreach (string error in errors)
                    Console.WriteLine(error);

                Console.ForegroundColor = color; 
            }
        }

        private static void Print(Stopwatch watch)
        {
            watch.Stop();

            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"{watch.ElapsedMilliseconds} ms\n");

            Console.ForegroundColor = color;
        }
    }
}
