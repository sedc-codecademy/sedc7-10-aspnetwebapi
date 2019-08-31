using DataAccess;
using DataModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository<UserDto> _userRepository;
        private readonly IRepository<NoteDto> _noteRepository;
        public NoteService(IRepository<UserDto> userRepository,
            IRepository<NoteDto> noteRepository)
        {
            _userRepository = userRepository;
            _noteRepository = noteRepository;
        }

        public void AddNote(NoteModel model)
        {
            var user = _userRepository.GetAll()
                .FirstOrDefault(x => x.Id == model.UserId);

            NoteDto note = new NoteDto()
            {
                UserId = user.Id,
                Color = model.Color,
                Text = model.Text,
                Tag = (int)model.Tag
            };

            _noteRepository.Add(note);
        }

        public void DeleteNote(int id, int userId)
        {
            var item = _noteRepository.GetAll()
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            _noteRepository.Delete(item);
        }

        public NoteModel GetNote(int id, int userId)
        {
            var item = _noteRepository.GetAll()
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            NoteModel note = new NoteModel()
            {
                Id = item.Id,
                UserId = item.UserId,
                Color = item.Color,
                Text = item.Text,
                Tag = (TagType)item.Tag
            };
            return note;
        }

        public IEnumerable<NoteModel> GetUserNotes(int userId)
        {
            return _noteRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => new NoteModel()
                {
                    Id = x.Id,
                    Color = x.Color,
                    Text = x.Text,
                    Tag = (TagType)x.Tag,
                    UserId = x.UserId
                });
        }
    }
}
