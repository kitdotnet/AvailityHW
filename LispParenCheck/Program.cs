using System;

namespace LispParenCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var checker = new ParenChecker();

            Console.Write("LISP string: ");
            var inputString = Console.ReadLine(); // e.g. " (write (* 2 3)) "
            while (inputString != "")
            {
                Console.WriteLine(checker.HasValidParens(inputString));
                Console.Write("LISP string: ");
                inputString = Console.ReadLine(); // e.g. " (write (* 2 3)) "
            }
        }
    }
}
