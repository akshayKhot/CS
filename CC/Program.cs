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

                var watch = Stopwatch.StartNew();

                if (string.IsNullOrWhiteSpace(line) || line == "clear")
                    return;

                var parser = new Parser(line);

                Print(parser.ParseTree);

                PrintErrorsIfAny(parser.Diagnostics);

                watch.Stop();

                PrintTime(watch);
            }
        }

        private static void Print(string tree)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(tree);

            Console.ForegroundColor = color;
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

        private static void PrintTime(Stopwatch watch)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"{watch.ElapsedMilliseconds} ms\n");

            Console.ForegroundColor = color;
        }
    }
}
