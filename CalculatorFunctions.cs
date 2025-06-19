using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


public static class CalculatorFunctions
{
    public static double FullCalculator(string input)
    {  
        // Full calculator - takes string input and returns the result.
        // If there is any error in the input, it throws appropriate exception
        // The calculator implements - +,-,*,/

        // Split string tokens
        List<string> tokens = SplitInput(input);

        // Validate tokens
        ValidateOperators(tokens);

        // Calculate
        double result = CalculateExpression(tokens);

        Console.WriteLine($"Result of the expression '{input}' is: {result}");

        return result;
    }

    public static List<string> SplitInput(string input)
        // This function Splits input string into numbers and non-numeric characters. For example - "960/43+44/54" -> ['960','/','43','+','44','54'].
        // Note: The function DOES NOT check if non-numeric characters are valid operators. For example - "960c43" -> ['960','c','43'].
        // It only splits based on numbers
    {
        List<string> result  = new List<string>();
        string currentNumber = "";

        foreach (char c in input)
        {
            if (char.IsDigit(c)) // add current character to the string if it's a digit.
            {
                currentNumber += c;
            }

            else // if not a digit - add currentNumber to the list and initialize currentNumber to be ""
            {
                if (currentNumber != "") 
                {
                    result.Add(currentNumber);
                    currentNumber = "";
                }

                result.Add(c.ToString()); // add this character to the list
            }

        }

        // Handle Last number 
        if (currentNumber != "")
        {
            result.Add(currentNumber);
        }

        return result;
    }

    public static void ValidateOperators(List<string> input)
    {
        // This function validates that:
        // 1. the non-numeric characters in the splitted input string are valied math operators.
        // If not - it throws an exception, noting which character is invalid
        // 2. no two valid operators [+,-,/,*,(,)] appear consecutively
        // 3. Check that the first and last characters are not arithmetic operators

        string[] validOperators = { "+", "-", "*", "/", "(", ")" };
        string[] arithmeticOperators = { "+", "-", "*", "/" };

        for (int i = 0; i< input.Count; i++)
        {
            string token = input[i];
            if (!double.TryParse(token, out _)) // if string is not a number
            {
                // 1. Check if it's a valid operator
                if (!validOperators.Contains(token)) 
                {
                    throw new Exception($"{token} is not a valid operator");
                }

                // 2. Check for consecutive arithmetic operators
                if (arithmeticOperators.Contains(token))
                {
                    if (i > 0 && arithmeticOperators.Contains(input[i - 1]))
                    {
                        throw new Exception($"Consecutive operators found: {input[i - 1]}{token} at index {i}");
                    }
                }

            }

        }

        // 3. Check that the first and last characters are not arithmetic operators
        if (input.Count > 0)
        {
            if (arithmeticOperators.Contains(input[0]))
            {
                throw new Exception($"The first character '{input[0]}' cannot be an arithmetic operator.");
            }
            if (arithmeticOperators.Contains(input[input.Count - 1]))
            {
                throw new Exception($"The last character '{input[input.Count - 1]}' cannot be an arithmetic operator.");
            }
        }


    }


    public static void ValidateBrackets(List<string> input)
    {
        // This function validates that if there are brackets - they are valid:
        // 1. No ) before ( opened.
        // 2. For each ( there is a ) in the end.
        // 3. before each ( there is an operator (*,\,-,+)
        // 4. after each ) there is an operator (*,\,-,+) 

        throw new Exception("Method isn't implemented yet.");
    }



    public static double CalculateExpression(List<string> tokens)
    {
        // This function calculates the expression given in tokens. It handles +,-,*,/ operators.
        // It assumes that the input has been validated and is in the correct format.

        double result = 0;

        // First handle *,/
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "*" || tokens[i] == "/")
            {
                double left = double.Parse(tokens[i - 1]);
                double right = double.Parse(tokens[i + 1]);
                
                if (tokens[i] == "*")
                {
                    result = left * right;
                }
                else if (tokens[i] == "/")
                {
                    if (right == 0)
                    {
                        throw new DivideByZeroException("Division by zero is impossible.");
                    }
                    result = left / right;
                }

                // Replace the three tokens (number, *\/, number) with the result
                tokens[i-1] = result.ToString();
                tokens.RemoveAt(i); // remove the operator
                tokens.RemoveAt(i); // remove the right number
                i--; // Adjust index, since we removed two elements
            }
        }

        // Then handle +,-
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "+" || tokens[i] == "-")
            {
                double left = double.Parse(tokens[i - 1]);
                double right = double.Parse(tokens[i + 1]);
                if (tokens[i] == "+")
                {
                    result = left + right;
                }
                else if (tokens[i] == "-")
                {
                    result = left - right;
                }
                // Replace the three tokens (number, +/-, number) with the result
                tokens[i - 1] = result.ToString();
                tokens.RemoveAt(i); // remove the operator
                tokens.RemoveAt(i); // remove the right number
                i--; // Adjust index, since we removed two elements
            }
        }

        double finalResult = double.Parse(string.Join("", tokens));


        return finalResult;
    }


    public static double CalculateExpressionWithBrackets(List<string> tokens)
    {
        // For each brackets, apply CalculateExpression.

        throw new Exception($"Method isn't implemented yet");
    }

}

