using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDo.Api.Controllers;
using ToDo.Api.Persistence;

namespace ToDo.Api.Tests
{
    [TestClass]
    public class TestToDoController
    {
        protected Mock<IRepository<Persistence.ToDo>> _mockRepository;
        protected ToDoController _sut;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Persistence.ToDo>>();
            _sut = new ToDoController(_mockRepository.Object);
            _sut.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, "Test User"),
                        new Claim(ClaimTypes.NameIdentifier, "Test.User@test.com")
                    }))
                }
            };

            _mockRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Persistence.ToDo>
            {
                new Persistence.ToDo(),
                new Persistence.ToDo()
            });
        }


        [TestMethod]
        public async Task post_saves_todo_to_repository()
        {
            var todo = new Persistence.ToDo();

            _mockRepository.Setup(x => x.SaveAsync(todo))
                                   .ReturnsAsync(todo)
                                   .Verifiable();

            var result = await _sut.Post(todo);
            result.Should().Be(todo);

            _mockRepository.Verify();
        }

        [TestMethod]
        public async Task put_updates_todo_to_repository()
        {
            var todo = new Persistence.ToDo
            {
                Id = 1,
                Content = "Updated Content"
            };

            _mockRepository.Setup(x => x.GetAsync(1))
                .ReturnsAsync(todo);

            _mockRepository.Setup(x => x.SaveAsync(todo))
                                  .ReturnsAsync(todo)
                                  .Verifiable();

            var result = await _sut.Put(1, todo);
            result.Should().Be(todo);

            _mockRepository.Verify();
        }

        [TestMethod]
        public async Task put_throws_expception_if_todo_not_found()
        {
            var todo = new Persistence.ToDo
            {
                Id = 1,
                Content = "Updated Content"
            };

            _mockRepository.Setup(x => x.GetAsync(1))
                .ReturnsAsync(default(Persistence.ToDo));

            _mockRepository.Setup(x => x.SaveAsync(todo))
                                  .ReturnsAsync(todo)
                                  .Verifiable();

            try
            {
                await _sut.Put(1, todo);
                Assert.Fail("Item not found exception not thrown");
            }
            catch (Exception)
            {
                _mockRepository.Verify(x => x.SaveAsync(It.IsAny<Persistence.ToDo>()), Times.Never);
            }
        }

        [TestMethod]
        public async Task get_returns_all_todo()
        {
            var result = await _sut.Get();
            result.Should().NotBeEmpty();

            _mockRepository.Verify();
        }

        [TestMethod]
        public async Task get_by_id_returns_corresponding_value()
        {
            var todo = new Persistence.ToDo();

            _mockRepository.Setup(x => x.GetAsync(1))
                .ReturnsAsync(todo);

            var result = await _sut.Get(1);
            result.Should().Be(todo);

            _mockRepository.Verify();
        }

        [TestMethod]
        public async Task delete_removes_corresponding_todo_from_repository()
        {
            var todo = new Persistence.ToDo();

            _mockRepository.Setup(x => x.RemoveAsync(todo));

            await _sut.Delete(1);

            _mockRepository.Verify();
        }
    }
}
