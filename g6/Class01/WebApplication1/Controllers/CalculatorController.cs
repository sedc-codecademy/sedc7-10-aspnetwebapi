using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("{op}/{first}/{second}")]
        public ActionResult<OperationResult> Get(string op, int first, int second)
        {
            var calculator = new Calculator();
            if (!calculator.CanMapToOperator(op)) {
                return UnprocessableEntity($"Cannot map {op} to a valid operation");
            }
            
            var operation = new Operation
            {
                Operator = calculator.MapStringToOperator(op),
                FirstArgument = first,
                SecondArgument = second
            };
            return calculator.ExecuteOperation(operation);
        }
    }
}