using SEDC.Loto3000.Models;

namespace SEDC.Loto3000.BusinessLayer.Contracts
{
    public interface IDrawService
    {
        Draw CreateNew(string adminEmail);

        Draw SubmitDraw(string adminEmail);
    }
}
