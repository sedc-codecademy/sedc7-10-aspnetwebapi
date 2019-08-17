using CalculatorWebApi.Models;
using CalculatorWebApi.Service;
using CalculatorWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("{op}/{first}/{second}")]
        public ActionResult<OperationViewModel> Get(string op, int first, int second)
        {
            // [SW] we're creating a new instance of the service for calculation.
            // The method in the controller should do input parsing and orchestration,
            // but it's a bad practice to use it for the processing work
            var calculator = new Calculator();

            // [SW] If we can can check whether the input is correct, we should do that
            // instead of relying on exception handling. Exceptions should be exceptional,
            // bad input from the user is NOT exceptional, and should be expected
            if (!calculator.CanMapToOperator(op))
            {
                // [SW] If the input is not up to par, we should return early, with a specific message
                return UnprocessableEntity($"Cannot map {op} to a valid operation");
            }

            // [SW] We're creating an input model from the input, once we know that the input is viable.
            // We can be sure that the `MapStringToOperator` call will not fail
            var operation = new Operation
            {
                Operator = calculator.MapStringToOperator(op),
                FirstArgument = first,
                SecondArgument = second
            };

            // [SW] Execute the operation and get the result model back
            var model = calculator.ExecuteOperation(operation);

            // [SW] Map the model to a view-model and then return the view-model
            return OperationViewModel.FromModel(model);
        }
    }
}
