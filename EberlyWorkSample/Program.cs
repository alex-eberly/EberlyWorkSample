using System;
using System.Collections.Generic;

namespace EberlyWorkSample
{
    public class Program
    {
        static void Main()
        {
            var dictionary = new Dictionary<string, List<string>>();
            Console.WriteLine("Welcome to my Multi-Value Dictionary App. Enter HELP to view available commands");
            Console.WriteLine("------------------------");
            Commands.ReadCommand(Console.ReadLine(), dictionary);
        }

        public static void ThrowError(string message)
        {
            Console.WriteLine($"ERROR: {message}");
        }
    }
}
