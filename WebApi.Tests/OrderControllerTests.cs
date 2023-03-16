using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.DTOs;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _orderRepoMock;
        private Fixture _fixture;
        private OrderController _controller;
        private Mock<IMapper> _mapper;

        public OrderControllerTests()
        {
            _fixture = new Fixture();
            _orderRepoMock = new Mock<IOrderService>();
            _mapper = new Mock<IMapper>();
        }

        [TestMethod]
        public void Post_Order_ReturningOK()
        {
            var order = _fixture.Create<OrderCreateDTO>();

            _orderRepoMock.Setup(repo => repo.Create(It.IsAny<Order>()));

            _controller = new OrderController(_orderRepoMock.Object , _mapper.Object);

            var result = _controller.Post(order);

            var obj = ((StatusCodeResult)result);

            Assert.AreEqual(200, obj.StatusCode);

        }
    }
}
