using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Models
{
    // [SW] This class is an input model - we map the data that we receive to data we can use in the application
    public class Operation
    {
        public Operator Operator { get; set; }
        public int FirstArgument { get; set; }
        public int SecondArgument { get; set; }
    }

    // [SW] This class is an output model - we calculate the data that we need to send to the client
    public class OperationResult : Operation
    {
        public int Result { get; set; }
    }
}
