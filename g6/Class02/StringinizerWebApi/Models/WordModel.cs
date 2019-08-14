using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringinizerWebApi.Models
{
    public class WordModel
    {
        public string UpperCase { get; set; }
        public string LowerCase { get; set; }

        public int SomeId { get; set; }

        public void SetValues(string input)
        {
            UpperCase = input.ToUpper();
            LowerCase = input.ToLower();
        }
    }
}
