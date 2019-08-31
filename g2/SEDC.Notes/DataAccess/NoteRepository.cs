using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class NoteRepository : IRepository<NoteDto>
    {
        private readonly NotesAppDbContext _context;
        public NoteRepository(NotesAppDbContext context)
        {
            _context = context;
        }

        public void Add(NoteDto entity)
        {
            _context.Notes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(NoteDto entity)
        {
            _context.Notes.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<NoteDto> GetAll()
        {
            return _context.Notes;
        }

        public void Update(NoteDto entity)
        {
            _context.Notes.Update(entity);
            _context.SaveChanges();
        }
    }
}
