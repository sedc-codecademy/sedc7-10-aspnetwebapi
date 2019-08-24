using SEDC.Loto3000.Models;
using System.Collections.Generic;

namespace SEDC.Loto3000.BusinessLayer.Contracts
{
    public interface IWinnerService
    {
        void SetWinners(string drawId);

        IEnumerable<Winner> GetWinners(string drawId);
    }
}
