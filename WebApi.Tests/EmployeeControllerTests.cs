using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Tests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeService> _employeeRepoMock;
        private Fixture _fixture;
        private EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _fixture = new Fixture();
            _employeeRepoMock = new Mock<IEmployeeService>();
        }

        [TestMethod]
        public void Get_Employee_ReturnOk()
        {

            var employeeeList = _fixture.CreateMany<Employee>(3).ToList();

            _employeeRepoMock.Setup(repo => repo.GetAll()).Returns(employeeeList);

            _controller = new EmployeeController(_employeeRepoMock.Object);

            var result = _controller.Get();

            var obj = result.Result as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public void Get_Employee_ThrowException()
        {
            _employeeRepoMock.Setup(repo => repo.GetAll()).Throws(new Exception());

            _controller = new EmployeeController(_employeeRepoMock.Object);

            var result = _controller.Get();

            var obj = result.Result as ObjectResult;

            Assert.AreEqual(400, obj.StatusCode);
        }
    }
}