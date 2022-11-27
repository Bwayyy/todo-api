using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Models.Todo;
using Todo.Domain.Entity;

namespace Todo.Test.Application.Todo
{
    public class TodoQueryTest
    {
        [Fact]
        public void TodoQueryParams_SortByTitleAsc_ShouldSuccess()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                SortBy = "title",
                SortDirection = "asc"
            };
            var expected = todos.OrderBy(x => x.Body.Title).ToList();
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(30).And.ContainInOrder(expected);
        }
        [Fact]
        public void TodoQueryParams_SortByDescriptionDesc_ShouldSuccess()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                SortBy = "description",
                SortDirection = "desc"
            };
            var expected = todos.OrderByDescending(x => x.Body.Description).ToList();
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(30).And.ContainInOrder(expected);
        }
        [Fact]
        public void TodoQueryParams_SortByDueDateAsc_ShouldSuccess()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                SortBy = "dueDate",
                SortDirection = "asc"
            };
            var expected = todos.OrderBy(x => x.Body.DueDate).ToList();
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(30).And.ContainInOrder(expected);
        }
        [Fact]
        public void TodoQueryParams_FilterByTitle_ShouldHaveExpectedItem()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                Filters = new TodoListFilter
                {
                    Title = todos[0].Body.Title,
                }
            };
            var expected = todos[0];
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(1).And.Contain(expected);
        }
        [Fact]
        public void TodoQueryParams_FilterByStatus_ShouldHaveExpectedItem()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var expectedStatus = TodoItemStatus.InProgress;
            var queryParams = new TodoQueryParams
            {
                Filters = new TodoListFilter
                {
                    Status = TodoItemStatus.InProgress,
                }
            };
            var expected = todos.Where(x=>x.Body.Status == expectedStatus).ToList();
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().Equal(result);
        }
        [Fact]
        public void TodoQueryParams_FilterByDescription_ShouldHaveExpectedItem()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                Filters = new TodoListFilter
                {
                    Description = todos[0].Body.Description,
                }
            };
            var expected = todos[0];
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(1).And.Contain(expected);
        }
    }
}
