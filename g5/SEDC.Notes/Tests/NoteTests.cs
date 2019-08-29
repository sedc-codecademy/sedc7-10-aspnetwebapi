using DataAccess;
using DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tests
{
    [TestClass]
    public class NoteTests
    {
        [TestMethod]
        public void GetAll_ValidUserId_AllNotesForThatUser()
        {
            // Arrange
            INoteService noteService = new NoteService(MockHelper.MockUserRepository(), MockHelper.MockNoteRepository());
            int expectedResult = 3;
            int userId = 1;
            // Act
            IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }
        [TestMethod]
        public void GetById_InvalidUserId_null()
        {
            // Arrange
            INoteService noteService = new NoteService(MockHelper.MockUserRepository(), MockHelper.MockNoteRepository());
            int expectedResult = 0;
            int userId = 3;
            // Act
            IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }
        [TestMethod]
        public void AddNote_ValidData_NoteAdded()
        {
            // Arrange
            INoteService noteService = new NoteService(MockHelper.MockUserRepository(), MockHelper.MockNoteRepository());
            int expectedResult = 4;
            NoteModel model = new NoteModel()
            {
                Id = 4,
                Text = "Test the app",
                Color = "red",
                Tag = TagType.Work,
                UserId = 1
            };
            // Act
            noteService.AddNote(model);
            // Assert
            IEnumerable<NoteModel> resultNotes = noteService.GetUserNotes(model.UserId);
            Assert.AreEqual(expectedResult, resultNotes.ToList().Count);
        }
    }
}
