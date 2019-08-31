using DataAccess;
using DataModels;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests
{
    public class NoteServiceTests
    {
        [Fact]
        public void GetUserNotes_UserWithNotes_ReturnsUserNotes()
        {
            //arrange
            var testUserId = 1;
            var notes = new List<NoteDto>
            {
                new NoteDto
                {
                    UserId = testUserId
                },
                new NoteDto
                {
                    UserId = testUserId
                },
                new NoteDto
                {
                    UserId = testUserId
                },
            };
            var noteService = new NoteService(new FakeUserRepository(Enumerable.Empty<UserDto>()), 
                                                new FakeNoteRepository(notes));

            //act
            var result = noteService.GetUserNotes(testUserId);

            //assert
            Assert.Equal(notes.Count(), result.Count());
        }

        [Fact]
        public void AddNote_NotExistingUser_ThrowsUserNotFoundException()
        {
            //arrange
            var notes = new List<NoteDto>();
            var noteService = new NoteService(new FakeUserRepository(Enumerable.Empty<UserDto>()), 
                new FakeNoteRepository(notes));

            //act

            //assert
            var note = new Models.NoteModel
            {
                UserId = 1
            };
            Assert.Throws<UserNotFoundException>(() => noteService.AddNote(note));
        }

        private class FakeNoteRepository : IRepository<NoteDto>
        {
            private readonly IEnumerable<NoteDto> _initialData;

            public FakeNoteRepository(IEnumerable<NoteDto> initialData)
            {
                _initialData = initialData;
            }
            public void Add(NoteDto entity)
            {
                throw new NotImplementedException();
            }

            public void Delete(NoteDto entity)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<NoteDto> GetAll()
            {
                return _initialData;
            }

            public void Update(NoteDto entity)
            {
                throw new NotImplementedException();
            }
        }

        private class FakeUserRepository : IRepository<UserDto>
        {
            private readonly IEnumerable<UserDto> _initialUsers;

            public FakeUserRepository(IEnumerable<UserDto> initialUsers)
            {
                _initialUsers = initialUsers;
            }
            public void Add(UserDto entity)
            {
                throw new NotImplementedException();
            }

            public void Delete(UserDto entity)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<UserDto> GetAll()
            {
                return _initialUsers;
            }

            public void Update(UserDto entity)
            {
                throw new NotImplementedException();
            }
        }
    }
}
