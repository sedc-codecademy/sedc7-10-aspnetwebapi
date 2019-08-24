using System;
using System.Collections.Generic;

namespace SEDC.Loto3000.Models
{
    public class Ticket : BaseModel
    {
        public Ticket()
        {
            Created = DateTime.UtcNow;
        }
        
        public IEnumerable<ushort> PickedNumbers { get; set; }

        public string UserId { get; set; }

        public string DrawId { get; set; }

        public DateTime Created { get; set; }
    }
}
