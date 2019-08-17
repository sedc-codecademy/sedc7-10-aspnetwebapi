using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Models
{
    // [SW] This enum represents the operators that we support.
    // Note that the enum is a C# construct, and we cannot receive it directly from the client,
    // nor can we send it directly to the client, yet, it's very useful to work with it in the application
    // This is used as an example of the mapping that we need to do to get data in/out of the application
    public enum Operator
    {
        Subtraction,
        Multiplication,
        Division,
        Addition
    }
}
