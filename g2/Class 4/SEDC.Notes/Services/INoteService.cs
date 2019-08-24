using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface INoteService
    {
        IEnumerable<NoteModel> GetUserNotes(int userId);
        NoteModel GetNote(int id, int userId);
        void AddNote(NoteModel model);
        void DeleteNote(int id, int userId);
    }
}
