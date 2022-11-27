using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DomainEntity.Todo;
using Todo.Application.Errors.Todo.Authorization;
using Todo.Application.Infrastructure.Repository;
using Todo.Application.Services.Todo;

namespace Todo.Test.Application.Todo
{
    public class TodoAuthorizationServiceTest
    {
        private readonly Mock<ITodoAccessRightRepo> _todoAccessRightRepo;
        public TodoAuthorizationServiceTest()
        {
            _todoAccessRightRepo= new Mock<ITodoAccessRightRepo>();
        }
        private void MockGetByIdAndTargetUser(TodoAccessRight? value)
        {
            _todoAccessRightRepo.Setup(x => x.GetByIdAndTargetUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(value);
        }
        [Fact]
        public void AuthorizeUser_WhenNoAccessRight_ShouldAdd()
        {
            //Arrange
            MockGetByIdAndTargetUser(null);
            var service = new TodoAuthorizationService(_todoAccessRightRepo.Object);
            Guid expectedUserId = Guid.NewGuid(), expectedTodoId = Guid.NewGuid();
            var expectedRights = new List<int> { 1, 2 };
            //Act
            var result = service.AuthorizeUser(expectedUserId, expectedTodoId, expectedRights);
            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.UserId.Should().Be(expectedUserId);
            result.Value.TodoId.Should().Be(expectedTodoId);
            result.Value.Rights.Select(x=>(int)x).Should().Equal(expectedRights);
        }
        [Fact]
        public void AuthorizeUser_WhenHaveExisting_ShouldUpdateExisting()
        {
            //Arrange
            Guid expectedId = Guid.NewGuid(),
                 expectedUserId = Guid.NewGuid(),
                 expectedTodoId = Guid.NewGuid();
            var expectedRights = new List<int> { 1, 2 };
            var existingRight = new TodoAccessRight
            {
                Id = expectedId,
                TodoId = expectedTodoId,
                UserId = expectedUserId,
                Rights = new List<AccessRight> { AccessRight.Read }
            };
            MockGetByIdAndTargetUser(existingRight);
            var service = new TodoAuthorizationService(_todoAccessRightRepo.Object);
            //Act
            var result = service.AuthorizeUser(expectedUserId, expectedTodoId, expectedRights);
            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Id.Should().Be(expectedId);
            result.Value.UserId.Should().Be(expectedUserId);
            result.Value.TodoId.Should().Be(expectedTodoId);
            result.Value.Rights.Select(x => (int)x).Should().Equal(expectedRights);
        }
        [Fact]
        public void ValidateAccess_WhenHaveAccess_ShouldSuccess()
        {
            MockGetByIdAndTargetUser(new TodoAccessRight
            {
                Rights = new List<AccessRight> { AccessRight.Write, AccessRight.Read }
            });
            var expectedRight = new List<int> { 1, 2 };
            var service = new TodoAuthorizationService(_todoAccessRightRepo.Object);
            //Action
            var result = service.ValidateAccess(Guid.NewGuid(), Guid.NewGuid(), expectedRight);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void ValidateAccess_WhenNoAccess_ShouldReturnAuthorizationError()
        {
            MockGetByIdAndTargetUser(null);
            var expectedRight = new List<int> { 1, 2 };
            var service = new TodoAuthorizationService(_todoAccessRightRepo.Object);
            //Action
            var result = service.ValidateAccess(Guid.NewGuid(), Guid.NewGuid(), expectedRight);
            //Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Should().BeOfType<TodoAuthorizationError>();

        }
    }
}
