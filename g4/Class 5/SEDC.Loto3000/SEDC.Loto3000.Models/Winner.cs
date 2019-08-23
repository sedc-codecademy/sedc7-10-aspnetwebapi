using SEDC.Loto3000.Models.Enums;

namespace SEDC.Loto3000.Models
{
    public class Winner : BaseModel
    {
        public string DrawId { get; set; }

        public string TicketId { get; set; }

        public Prize Prize { get; set; }
    }
}
