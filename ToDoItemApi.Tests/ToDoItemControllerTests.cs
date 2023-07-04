using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using ToDo.Api.Controllers;
using ToDo.Api.Services.Services.Contracts;
using ToDo.Models.Dtos;
using ToDo.Models.Entities;
using ToDo.Models.Payloads;

namespace ToDoItemApi.Tests
{
    public class ToDoItemControllerTests
    {
        #region GetItems

        [Fact]
        public async Task GetItems_ReturnsOkResult_WhenItemsExist()
        {
            // Arrange
            var mockToDoItemService = new Mock<IToDoItemService>();
            var expectedItems = new List<ToDoItem>
            {
                new ToDoItem { Id = 1, Name = "Test1" },
                new ToDoItem { Id = 2, Name = "Test2" }
            };
            mockToDoItemService.Setup(x => x.GetItems()).ReturnsAsync(expectedItems);

            var controller = new ToDoItemController(mockToDoItemService.Object);

            // Act
            var result = await controller.GetItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var item = Assert.IsType<List<ToDoItemDto>>(okResult.Value);

            Assert.Equal(expectedItems.Count, item.Count);
        }

        [Fact]
        public async Task GetItems_ReturnsNotFound_WhenItemsNotFound()
        {
            // Arrange
            var mockToDoItemService = new Mock<IToDoItemService>();
            mockToDoItemService.Setup(x => x.GetItems()).ReturnsAsync((List<ToDoItem>)null);

            var controller = new ToDoItemController(mockToDoItemService.Object);

            // Act
            var result = await controller.GetItems();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region GetItem

        [Fact]
        public async Task GetItem_ReturnOkResult_WhenItemExist()
        {
            // Arrange
            var mockToDoItemService = new Mock<IToDoItemService>();
            var exceptedItem = new ToDoItem
            {
                Id = 1,
                Name = "test1",
                Description = "testDescription"
            };
            mockToDoItemService.Setup(x => x.GetItem(1)).ReturnsAsync(exceptedItem);

            var controller = new ToDoItemController(mockToDoItemService.Object);

            // Act
            var result = await controller.GetItem(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var item = Assert.IsType<ToDoItemDto>(okResult.Value);

            Assert.Equal(1, item.Id);
        }

        [Fact]
        public async Task GetItem_ReturnsNotFound_WhenItemNotFound()
        {
            // Arrange
            var mockToDoItemService = new Mock<IToDoItemService>();
            mockToDoItemService.Setup(x => x.GetItem(1)).ReturnsAsync((ToDoItem)null);

            var controller = new ToDoItemController(mockToDoItemService.Object);

            // Act
            var result = await controller.GetItem(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        #endregion

        #region PostItem

        [Fact]
        public async Task PostItem_ReturnsOkResult_WhenItemPosted()
        {
            // Arrange
            var mockToDoItemService = new Mock<IToDoItemService>();
            var exampleToDoItemPayload = new ToDoItemPayload
            {
                Name = "Test",
                Description = "TestDescription",
            };

            var exceptToDoItem = new ToDoItem
            {
                Name = "Test",
                Description = "TestDescription"
            };
            mockToDoItemService.Setup(x => x.PostItem(exampleToDoItemPayload)).ReturnsAsync(exceptToDoItem);

            var controller = new ToDoItemController(mockToDoItemService.Object);
            // Act
            var result = await controller.PostItem(exampleToDoItemPayload);

            // Assert
            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var item = Assert.IsType<ToDoItemDto>(okResult.Value);

            Assert.Equal("Test", item.Name);
            Assert.Equal("TestDescription", item.Description);
            Assert.Equal(exampleToDoItemPayload.Name, item.Name);
            Assert.Equal(exampleToDoItemPayload.Description, item.Description);
        }

        [Fact]
        public async Task PostItem_ReturnException_WhenPayloadIsNull()
        {
            // Arrange
            var mockToDoItemService = new Mock<IToDoItemService>();
            mockToDoItemService.Setup(x => x.PostItem(null)).ReturnsAsync((ToDoItem)null);

            var controller = new ToDoItemController(mockToDoItemService.Object);
            // Act
            var result = await controller.PostItem(null);

            // Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => controller.PostItem(null)).Status;

            Assert.Null(result.Value);
            Assert.Equal(TaskStatus.Faulted, ex);
        }

        #endregion

        #region DeleteItem

        [Fact]
        public async Task DeleteItem_ReturnOk_WhenItemDeleted()
        {
            // Arrange
            var exampleToDoItem = new ToDoItem
            {
                Id = 1,
                Name = "Test",
                Description = "Test1",
            };

            var mockToDoItemService = new Mock<IToDoItemService>();
            mockToDoItemService.Setup(x => x.DeleteItem(exampleToDoItem));

            var controller = new ToDoItemController(mockToDoItemService.Object);
            // Act
            var result = await controller.DeleteItem(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
        #endregion
    }
}
