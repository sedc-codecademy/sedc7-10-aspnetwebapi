using SEDC.Loto3000.BusinessLayer.Contracts;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System;
using System.Collections.Generic;

namespace SEDC.Loto3000.BusinessLayer.Implementations
{
    public class DrawService : IDrawService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDrawRepository _drawRepository;
        private readonly IGenericRepository<Draw> _drawGenericRepository;
        private readonly Random _random = new Random();

        public DrawService(IUserRepository userRepository, IDrawRepository drawRepository,
                            IGenericRepository<Draw> drawGenericRepository)
        {
            _userRepository = userRepository;
            _drawRepository = drawRepository;
            _drawGenericRepository = drawGenericRepository;
        }

        public Draw CreateNew(string adminEmail)
        {
            var user = _userRepository.GetUser(adminEmail);
            if (IsUserNullOrNotAdmin(user))
                throw new Exception($"User with email: {adminEmail} does not exist or is not admin");

            var activeDraw = _drawRepository.GetActiveDraw();
            if (activeDraw != null)
                throw new Exception($"There is already active draw");

            var draw = new Draw
            {
                InitiatedBy = user.Id,
                IsActive = true
            };
            _drawGenericRepository.Add(draw);
            return draw;
        }

        public Draw SubmitDraw(string adminEmail)
        {
            var user = _userRepository.GetUser(adminEmail);
            if (IsUserNullOrNotAdmin(user))
                throw new Exception($"User with email: {adminEmail} does not exist or is not admin");
            
            var activeDraw = _drawRepository.GetActiveDraw();
            activeDraw.DrawnNumbers = GetDrawnNumbers();
            activeDraw.IsActive = false;

            _drawGenericRepository.Update(activeDraw);
            return activeDraw;
        }

        private bool IsUserNullOrNotAdmin(User user)
        {
            return user == null || !user.IsAdmin;
        }

        private IEnumerable<ushort> GetDrawnNumbers()
        {
            var result = new HashSet<ushort>();
            
            for (int i = 0; i < 7; i++)
            {
                bool added = result.Add(GenerateRandomInRange(1, 37));
                while (!added)
                    added = result.Add(GenerateRandomInRange(1, 37));
            }
            return result;
        }

        private ushort GenerateRandomInRange(int min, int max)
        {
            return (ushort)_random.Next(min, max + 1);
        }
    }
}
