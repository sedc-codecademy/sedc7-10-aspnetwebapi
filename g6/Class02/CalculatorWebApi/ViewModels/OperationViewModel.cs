using CalculatorWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.ViewModels
{
    // [SW] This is a view model - a plain simple class that we can send unchanged to the client 
    // (unlike the corresponding model)
    public class OperationViewModel
    {
        // [SW] Private constructor so that there is no way to create this class outside of it
        private OperationViewModel() { }

        public string Operator { get; set; }
        public int FirstArgument { get; set; }
        public int SecondArgument { get; set; }
        public int Result { get; set; }

        // [SW] A static method that transforms the data from a model (that can be used throughout the application)
        // to a view-model, that should be used only to send data to the client
        public static OperationViewModel FromModel(OperationResult model)
        {
            return new OperationViewModel
            {
                FirstArgument = model.FirstArgument,
                SecondArgument = model.SecondArgument,
                Result = model.Result,
                // [SW] here we transform the Operator property from a enum (that only makes sense in the C# code)
                // to a string that is both human readable and platform independent
                Operator = model.Operator.ToString().ToLower()
            };
        }
    }
}
