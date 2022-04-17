using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter expression: ");
            var expression = Console.ReadLine();
            var splitedExpression = expression.Split(' ');
            if(splitedExpression.Length > 3)
            {
                Console.WriteLine("Too hard expression");
                return;
            }
            if(splitedExpression.Length < 3)
            {
                Console.WriteLine("Invalid expression");
                return;
            }
            var firstValue = int.Parse(splitedExpression[0]);
            var secondValue = int.Parse(splitedExpression[2]);
            var sign = splitedExpression[1];
            
            if(firstValue < 0 || firstValue > 500)
            {
                Console.WriteLine("First value bigger than 500 or less than zero ");
                Console.ReadLine();
                return;
            }
            if(secondValue < 0 || secondValue > 500)
            {
                Console.WriteLine("Second value bigger than 500 or less than zero ");
                Console.ReadLine();
                return;
            }
            
            var types = typeof(Program).Assembly.GetTypes();
            var result = 0;

            switch(sign)
            {
                case "+":
                    PlusAction plusAction = null;
                    foreach(var type in types)
                    {
                        var baseType = type.BaseType == null ? "" : type.BaseType.Name;
                        if(baseType == "PlusAction")
                        {
                            var splitedName = type.Name.Split("Plus");
                            if(splitedName[0] == $"Action{firstValue.ToString()}" && splitedName[1] == secondValue.ToString())
                            {
                                plusAction = type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as PlusAction;
                                break;
                            }
                        }
                    }
                    result = plusAction.Process();
                break;
                case "-":
                    MinusAction minusAction = null;
                    foreach(var type in types)
                    {
                        var baseType = type.BaseType == null ? "" : type.BaseType.Name;
                        if(baseType == "MinusAction")
                        {
                            var splitedName = type.Name.Split("Minus");
                            if(splitedName[0] == $"Action{firstValue.ToString()}" && splitedName[1] == secondValue.ToString())
                            {
                                minusAction = type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as MinusAction;
                                break;
                            }
                        }
                    }
                    result = minusAction.Process();
                break;
                case "/":
                    if(secondValue == 0)
                    {
                        Console.WriteLine("Error (divide by zero)");
                        Console.ReadLine();
                        return;
                    }
                    DivideAction divideAction = null;
                    foreach(var type in types)
                    {
                        var baseType = type.BaseType == null ? "" : type.BaseType.Name;
                        if(baseType == "DivideAction")
                        {
                            var splitedName = type.Name.Split("Divide");
                            if(splitedName[0] == $"Action{firstValue.ToString()}" && splitedName[1] == secondValue.ToString())
                            {
                                divideAction = type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as DivideAction;
                                break;
                            }
                        }
                    }
                    result = divideAction.Process();
                break;
                case "*":
                    MultiplyAction multiplyAction = null;
                    foreach(var type in types)
                    {
                        var baseType = type.BaseType == null ? "" : type.BaseType.Name;
                        if(baseType == "MultiplyAction")
                        {
                            var splitedName = type.Name.Split("Multiply");
                            if(splitedName[0] == $"Action{firstValue.ToString()}" && splitedName[1] == secondValue.ToString())
                            {
                                multiplyAction = type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as MultiplyAction;
                                break;
                            }
                        }
                    }
                    result = multiplyAction.Process();
                break;
                default:
                Console.WriteLine("Wrong sign!");
                return;
            }
            
            Console.WriteLine($"Result: {result}");
            Console.ReadLine();
        }
    }
}