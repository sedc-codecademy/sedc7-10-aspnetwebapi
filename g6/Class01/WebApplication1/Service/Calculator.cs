using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class Calculator
    {
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

        public OperationResult ExecuteOperation(Operation operation)
        {
            int result;
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

        public bool CanMapToOperator(string op)
        {
            return mapping.ContainsKey(op.ToLower());
        }

        public Operator MapStringToOperator(string op)
        {
            if (!mapping.ContainsKey(op.ToLower()))
            {
                throw new ApplicationException($"Cannot map {op} to a valid operation");
            }
            return mapping[op.ToLower()];
        }
    }
}
