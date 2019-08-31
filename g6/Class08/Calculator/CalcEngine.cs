using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalcEngine
    {
        public void Add(int first, int second)
        {
            var result = first + second;
            Console.WriteLine(result);
        }

        public int Multiply(int first, int second)
        {
            var result = first * second;
            return result;
        }

        public double Multiply(double first, double second)
        {
            var result = first * second;
            return result;
        }

        public int Divide(int first, int second)
        {
            if (second == 0)
            {
                throw new ApplicationException("The second argument in the divide call was zero");
            }
            var result = first / second;
            return result;
        }
    }
}
