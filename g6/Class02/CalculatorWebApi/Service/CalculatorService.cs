using CalculatorWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Service
{
    public class Calculator
    {
        // [SW] This dictionary is used to map the string that comes from the client
        // to an Operator that we can use in the application. In this way we can easily support
        // multiple different formats of usage, while maintaining the exact same code on the server
        Dictionary<string, Operator> mapping = new Dictionary<string, Operator>
        {
            { "add", Operator.Addition },
            { "addition", Operator.Addition },
            { "plus", Operator.Addition },
            { "+", Operator.Addition },
            { "subtract", Operator.Subtraction},
            { "sub", Operator.Subtraction},
            { "minus", Operator.Subtraction},
            { "-", Operator.Subtraction},
            { "mul", Operator.Multiplication},
            { "multiply", Operator.Multiplication},
            { "times", Operator.Multiplication},
            { "*", Operator.Multiplication},
            { "divide", Operator.Division},
            { "div", Operator.Division},
            { "division", Operator.Division},
        };

        // [SW] This method is used to transform the input to the output. We can add other and different
        // calculations to our small engine, and the only change should be within this method.
        public OperationResult ExecuteOperation(Operation operation)
        {
            int result;
            // [SW] I'm using nested `if`s to manage the calculation. We could just as well use
            // a `switch`-`case` construct, or even something more complicated. Feel free to experiment
            if (operation.Operator == Operator.Addition)
            {
                result = operation.FirstArgument + operation.SecondArgument;
            }
            else if (operation.Operator == Operator.Subtraction)
            {
                result = operation.FirstArgument - operation.SecondArgument;
            }
            else if (operation.Operator == Operator.Multiplication)
            {
                result = operation.FirstArgument * operation.SecondArgument;
            }
            else if (operation.Operator == Operator.Division)
            {
                result = operation.FirstArgument / operation.SecondArgument;
            }
            else
            {
                // [SW] If the value of the operator is not one of the possible values (a case that is technically
                // possible, but should NEVER happen in real life), throw an exception. We have no idea how to proceed now.
                throw new ApplicationException($"Invalid operator value {operation.Operator}");
            }
            return new OperationResult
            {
                Operator = operation.Operator,
                FirstArgument = operation.FirstArgument,
                SecondArgument = operation.SecondArgument,
                Result = result
            };
        }

        // [SW] A method that enables the user to check whether the user can map the input to an operator.
        public bool CanMapToOperator(string op)
        {
            return mapping.ContainsKey(op.ToLower());
        }

        // [SW] A method that actually maps the input to an operator.
        public Operator MapStringToOperator(string op)
        {
            if (!mapping.ContainsKey(op.ToLower()))
            {
                // [SW] If we're unable to execute the mapping, throw an exception. Since we're exposing a way for the user to 
                // check if this will succeed, this exception should never occur. The only way for this exception to occur
                // is by an developer error.
                throw new ApplicationException($"Cannot map {op} to a valid operation");
            }
            return mapping[op.ToLower()];
        }
    }
}
