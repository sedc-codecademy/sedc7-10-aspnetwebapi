using System;
using System.Collections.Generic;

namespace SEDC.Loto3000.Models
{
    public class Draw : BaseModel
    {
        public Draw()
        {
            Created = DateTime.UtcNow;
            AreWinnersSet = false;
        }
        
        public IEnumerable<ushort> DrawnNumbers { get; set; }

        public DateTime Created { get; set; }

        public string InitiatedBy { get; set; }

        public bool IsActive { get; set; }

        public bool AreWinnersSet { get; set; }
    }
}
