using System;

namespace LispParenCheck
{
    public class ParenChecker
    {
        /// <summary>
        /// Returns true if parentheses are valid for a LISP code, e.g. " (write (* 2 3)) "
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>true if valid</returns>
        public bool HasValidParens(string inputString)
        {
            string expr = inputString.Trim(); //*** Leading and trailing whitespace should be disregarded.

            //*** A LISP expression should start with '(' and end with ')'.
            if (expr.StartsWith("(") == false ||
                expr.EndsWith(")") == false)
            {
                Console.WriteLine("false -- LISP expression should start with '(' and end with ')'.");
                return false;
            }

            //*** Each '(' should be followed by a corresponding closing ')'.
            int imbalance = 0; //open parenthecation count

            foreach (char c in expr.ToCharArray())
            {
                if (c == '(')
                    imbalance++;
                if (c == ')')
                    imbalance--;

                //Closing ')' should never precede a matching '('.
                if (imbalance < 0)
                {
                    Console.WriteLine("false -- closing ')' should never precede a matching '('.");
                    return false;
                }
            }
            Console.WriteLine(imbalance == 0
                ? "true -- valid LISP parentheses"
                : "false -- each '(' should be followed by a corresponding closing ')'.");
            return imbalance == 0;
        }
    }
}
