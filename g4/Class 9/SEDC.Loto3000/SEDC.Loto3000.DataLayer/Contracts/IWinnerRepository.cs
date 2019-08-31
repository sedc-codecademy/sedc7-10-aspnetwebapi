using SEDC.Loto3000.Models;
using System.Collections.Generic;

namespace SEDC.Loto3000.DataLayer.Contracts
{
    public interface IWinnerRepository
    {
        IEnumerable<Winner> GetByDrawId(string drawId);

        void AddMany(IEnumerable<Winner> winners);
    }
}
