using DataAccess;
using DataModels;
using Moq;
using Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests.MoqTests
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
            var mockUserRepository = new Mock<IRepository<UserDto>>();
            var mockNoteRepository = new Mock<IRepository<NoteDto>>();
            mockNoteRepository.Setup(repo => repo.GetAll())
                                .Returns(notes);

            var noteService = new NoteService(mockUserRepository.Object,
                                                mockNoteRepository.Object);

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
            var mockUserRepository = new Mock<IRepository<UserDto>>();
            mockUserRepository.Setup(repo => repo.GetAll())
                                .Returns(Enumerable.Empty<UserDto>);
            var mockNoteRepository = new Mock<IRepository<NoteDto>>();
            var noteService = new NoteService(mockUserRepository.Object,
                                                mockNoteRepository.Object);

            //act

            //assert
            var note = new Models.NoteModel
            {
                UserId = 1
            };
            Assert.Throws<UserNotFoundException>(() => noteService.AddNote(note));
        }

        [Fact]
        public void AddNote_ValidData_SuccessfulAddedNote()
        {
            //arrange
            var notes = new List<NoteDto>();
            var mockUserRepository = new Mock<IRepository<UserDto>>();
            var testUserId = 1;
            var users = new List<UserDto>
            {
                new UserDto
                {
                    Id = testUserId
                }
            };
            mockUserRepository.Setup(repo => repo.GetAll())
                                .Returns(users);
            var mockNoteRepository = new Mock<IRepository<NoteDto>>();
            mockNoteRepository.Setup(repo => repo.Add(It.IsAny<NoteDto>()))
                                .Callback((NoteDto note) => notes.Add(note));

            var noteService = new NoteService(mockUserRepository.Object,
                                                mockNoteRepository.Object);

            //act
            noteService.AddNote(new Models.NoteModel
            {
                Id = 1,
                UserId = testUserId
            });

            //assert
            Assert.Single(notes);
        }

    }
}
