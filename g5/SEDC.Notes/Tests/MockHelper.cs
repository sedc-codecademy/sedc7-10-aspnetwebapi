﻿using DataAccess;
using DataModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tests
{
    public static class MockHelper
    {
        public static IRepository<UserDto> MockUserRepository()
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);
            List<UserDto> users = new List<UserDto>()
            {
                new UserDto(){
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Username = "bob007",
                    Password = hashedPassword
                }
            };
            Mock<IRepository<UserDto>> mockUserRepository = new Mock<IRepository<UserDto>>();

            mockUserRepository.Setup(x => x.GetAll()).Returns(users);

            mockUserRepository.Setup(x => x.Add(
                It.IsAny<UserDto>())).Callback((UserDto note) =>
                {
                    users.Add(note);
                });

            mockUserRepository.Setup(x => x.Update(
                It.IsAny<UserDto>())).Callback((UserDto note) =>
                {
                    users.Add(note);
                });

            mockUserRepository.Setup(x => x.Delete(
                It.IsAny<UserDto>())).Callback((UserDto note) =>
                {
                    users.Remove(note);
                });
            return mockUserRepository.Object;
        }
        public static IRepository<NoteDto> MockNoteRepository()
        {
            List<NoteDto> notes = new List<NoteDto>()
            {
                new NoteDto(){
                    Id = 1,
                    Text = "Don't forget to water the plant",
                    Color = "blue",
                    Tag = 2,
                    UserId = 1
                },
                new NoteDto(){
                    Id = 2,
                    Text = "Drink more Tea",
                    Color = "yellow",
                    Tag = 3,
                    UserId = 1
                },
                new NoteDto(){
                    Id = 5,
                    Text = "Make a break every 1h of coding",
                    Color = "red",
                    Tag = 1,
                    UserId = 1
                }
            };
            Mock<IRepository<NoteDto>> mockNotesRepository = new Mock<IRepository<NoteDto>>();

            mockNotesRepository.Setup(x => x.GetAll()).Returns(notes);

            mockNotesRepository.Setup(x => x.Add(
                It.IsAny<NoteDto>())).Callback((NoteDto note) =>
                {
                    notes.Add(note);
                });

            mockNotesRepository.Setup(x => x.Update(
                It.IsAny<NoteDto>())).Callback((NoteDto note) =>
                {
                    notes.Add(note);
                });

            mockNotesRepository.Setup(x => x.Delete(
                It.IsAny<NoteDto>())).Callback((NoteDto note) =>
                {
                    notes.Remove(note);
                });
            return mockNotesRepository.Object;
        }
    }
}
