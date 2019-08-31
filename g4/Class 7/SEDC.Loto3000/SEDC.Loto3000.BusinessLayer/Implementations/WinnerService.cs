using System;
using System.Collections.Generic;
using System.Linq;
using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using SEDC.Loto3000.Models.Enums;

namespace SEDC.Loto3000.BusinessLayer.Implementations
{
    public class WinnerService : IWinnerService
    {
        private readonly IGenericRepository<Winner> _winnersGenericRepository;
        private readonly IWinnerRepository _winnerRepository;
        private readonly IGenericRepository<Draw> _drawRepository;
        private readonly ITicketRepository _ticketRepository;

        public WinnerService(IGenericRepository<Winner> winnersGenericRepository,
                                IWinnerRepository winnerRepository,
                                IGenericRepository<Draw> drawRepository,
                                ITicketRepository ticketRepository)
        {
            _winnersGenericRepository = winnersGenericRepository;
            _winnerRepository = winnerRepository;
            _drawRepository = drawRepository;
            _ticketRepository = ticketRepository;
        }

        public IEnumerable<Winner> GetWinners(string drawId)
        {
            return _winnerRepository.GetByDrawId(drawId);
        }

        public void SetWinners(string drawId)
        {
            var draw = _drawRepository.GetById(drawId);
            if (draw == null)
                throw new ArgumentException($"There is no draw with id: {drawId}");

            if (draw.IsActive)
                throw new ArgumentException($"The draw is still active");

            if (draw.AreWinnersSet)
                return;
            var tickets = _ticketRepository.GetByDrawId(drawId);
            if (tickets == null || tickets.Count() == 0)
                return;

            var winners = new List<Winner>();
            foreach (var ticket in tickets)
            {
                var winningNumbers = GetWinningNumbers(ticket.PickedNumbers, draw.DrawnNumbers);
                if (winningNumbers.Count() < 3)
                    continue;

                var winner = new Winner
                {
                    DrawId = drawId,
                    TicketId = ticket.Id,
                    Prize = GetPrizeForWinningNumbersCount(winningNumbers.Count()),
                    WinningNumbers = winningNumbers
                };
                winners.Add(winner);
            }

            _winnerRepository.AddMany(winners);

            draw.AreWinnersSet = true;
            _drawRepository.Update(draw);
        }

        private Prize GetPrizeForWinningNumbersCount(int count)
        {
            if (Enum.IsDefined(typeof(Prize), count))
                return (Prize)count;

            throw new ArgumentException($"Value: {count} is not defined in the enum Prize");
        }

        private IEnumerable<ushort> GetWinningNumbers(IEnumerable<ushort> pickedNumbers, IEnumerable<ushort> drawnNumbers)
        {
            return drawnNumbers.Intersect(pickedNumbers);
        }
    }
}
