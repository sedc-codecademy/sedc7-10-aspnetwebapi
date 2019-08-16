using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringinizerWebApi.Models
{
    // [SW] This class is a model that we use in the application
    public class WordModel
    {
        public string UpperCase { get; set; }
        public string LowerCase { get; set; }

        public int SomeId { get; set; }

        // [SW] Since we've decided that we won't do the text transformations in a separate service, we've put
        // that code in our mode, thus turning it from an anemic model to a smart model.
        public void SetValues(string input)
        {
            UpperCase = input.ToUpper();
            LowerCase = input.ToLower();
        }
    }
}
