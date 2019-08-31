using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Services;
using Services.Exceptions;
using ToDoApp_Tests.FakeRepositories;
using Xunit;

namespace ToDoApp_Tests
{
    public class ToDoItemServiceTests
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoItemServiceTests()
        {
            var fakeToDoRepo = new FakeToDoItemRepository();
            var fakeUserRepo = new FakeUserRepository();
            _toDoItemService = new ToDoItemService(fakeToDoRepo, fakeUserRepo);
        }

        [Fact]
        public void AddToDoItem_EmptyTitle_ThrowsException()
        {
            //Arrange
            var model = new ToDoModel()
            {
                Description = "Test",
                Title = string.Empty,
                UserId = 5
            };

            Action action = () => _toDoItemService.AddToDoItem(model);

            var exception = Assert.Throws<ToDoException>(action);
            Assert.Equal("Title is required field", exception.Message);
        }

        [Fact]
        public void AddToDoItem_SuccessfulAdding()
        {
            //Arrange
            var model = new ToDoModel()
            {
                Description = "Test",
                Title = "Test",
                UserId = 1
            };

            _toDoItemService.AddToDoItem(model);
            Assert.True(true);
        }
    }
}
