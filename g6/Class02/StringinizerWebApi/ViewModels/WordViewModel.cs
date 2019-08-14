using StringinizerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringinizerWebApi.ViewModels
{
    public class WordViewModel
    {
        private WordViewModel() { }

        public string UpperCase { get; set; }
        public string LowerCase { get; set; }

        public static WordViewModel FromModel(WordModel model)
        {
            return new WordViewModel
            {
                UpperCase = model.UpperCase,
                LowerCase = model.LowerCase
            };
        }
    }
}
