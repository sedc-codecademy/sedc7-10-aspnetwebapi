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
            var calculator = new Calculator();
            if (!calculator.CanMapToOperator(op))
            {
                return UnprocessableEntity($"Cannot map {op} to a valid operation");
            }

            var operation = new Operation
            {
                Operator = calculator.MapStringToOperator(op),
                FirstArgument = first,
                SecondArgument = second
            };

            var model = calculator.ExecuteOperation(operation);

            return OperationViewModel.FromModel(model);
        }
    }
}
