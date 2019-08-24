using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public TagType Tag { get; set; }
        public int UserId { get; set; }
    }

    public enum TagType
    {
        Work = 1, Education = 2, Home = 3, Misc = 4, Other = 5
    }
}
