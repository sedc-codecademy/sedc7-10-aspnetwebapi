using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.G5.Class3.Domain
{
    public class Note
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
