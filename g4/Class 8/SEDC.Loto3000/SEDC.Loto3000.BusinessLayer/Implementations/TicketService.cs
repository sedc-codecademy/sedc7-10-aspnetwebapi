using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.Loto3000.BusinessLayer.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDrawRepository _drawRepository;
        private readonly IGenericRepository<Ticket> _ticketRepository;

        public TicketService(IUserRepository userRepository, IDrawRepository drawRepository,
                                IGenericRepository<Ticket> ticketRepository)
        {
            _userRepository = userRepository;
            _drawRepository = drawRepository;
            _ticketRepository = ticketRepository;
        }
        public Ticket SubmitTicket(IEnumerable<ushort> pickedNumbers, string userEmail)
        {
            ThrowIfNotValidPickedNumbers(pickedNumbers);

            var user = _userRepository.GetUser(userEmail);
            if (user == null)
                throw new ArgumentException($"User email: {userEmail} does not exist");

            var activeDraw = _drawRepository.GetActiveDraw();
            if (activeDraw == null)
                throw new Exception("There is no active draw");

            var ticket = new Ticket
            {
                DrawId = activeDraw.Id,
                PickedNumbers = pickedNumbers,
                UserId = user.Id
            };
            _ticketRepository.Add(ticket);

            return ticket;
        }

        private void ThrowIfNotValidPickedNumbers(IEnumerable<ushort> pickedNumbers)
        {
            if (pickedNumbers?.Count() != 7)
                throw new Exception("Picked numbers count must be equal to 7");

            if (pickedNumbers.Any(n => n < 1 || n > 37))
                throw new Exception("Picked numbers values must be between 1 and 37 inclusive");

            if (ContainsDuplicates(pickedNumbers))
                throw new Exception("Picked numbers can not contain duplicates");
        }

        private bool ContainsDuplicates(IEnumerable<ushort> numbers)
        {
            var uniques = numbers.Distinct();
            return numbers.Count() != uniques.Count();
        }
    }
}
