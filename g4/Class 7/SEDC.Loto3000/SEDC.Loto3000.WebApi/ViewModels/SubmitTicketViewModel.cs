using System.Collections.Generic;

namespace SEDC.Loto3000.WebApi.ViewModels
{
    public class SubmitTicketViewModel
    {
        public IEnumerable<ushort> PickedNumbers { get; set; }

        //public string UserEmail { get; set; }
    }
}
