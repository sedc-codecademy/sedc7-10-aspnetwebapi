using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Operation
    {
        public Operator Operator { get; set; }
        public int FirstArgument { get; set; }
        public int SecondArgument { get; set; }
    }

    public class OperationResult: Operation
    {
        public int Result { get; set; }
    }
}
