// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


// Check Split Input
string input = "17/2+3/2/2";
List<string> parts = CalculatorFunctions.SplitInput(input);
CalculatorFunctions.ValidateOperators(parts);

List<string> result = CalculatorFunctions.CalculateExpression(parts);
Console.WriteLine($"Result of the expression '{input}' is: {string.Join("", result)}");


//Console.WriteLine($"string input: {input}.\n string output:");
//foreach(string part in parts)
//{
//    Console.WriteLine(part);
//}