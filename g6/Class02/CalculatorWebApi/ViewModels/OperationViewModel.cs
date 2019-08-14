using CalculatorWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.ViewModels
{
    public class OperationViewModel
    {
        private OperationViewModel() { }

        public string Operator { get; set; }
        public int FirstArgument { get; set; }
        public int SecondArgument { get; set; }
        public int Result { get; set; }

        public static OperationViewModel FromModel(OperationResult model)
        {
            return new OperationViewModel
            {
                FirstArgument = model.FirstArgument,
                SecondArgument = model.SecondArgument,
                Result = model.Result,
                Operator = model.Operator.ToString().ToLower()
            };
        }
    }
}
