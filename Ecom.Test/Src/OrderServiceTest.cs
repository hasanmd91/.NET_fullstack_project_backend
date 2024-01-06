using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Moq;

namespace Ecom.Test.Src
{
    public class OrderServiceTest
    {
        private static IMapper _mapper;

        public OrderServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            IMapper mapper = configuration.CreateMapper();
            _mapper = mapper;

        }

        [Fact]
        public async void GetAllOrder_withValidLimitAndOffset_ShouldInvokedRepoMethod()
        {
            var repo = new Mock<IOrderRepo>();
            var emailService = new Mock<IEmailService>();
            GetAllParams options = new() { Limit = 20, Offset = 0 };
            var orderService = new OrderService(repo.Object, _mapper, emailService.Object);
            await orderService.GetAllOrderAsync(options);
            repo.Verify(repo => repo.GetAllOrderAsync(options), Times.Once);
        }


        [Fact]
        public async void GetAllOrderAsync_ShouldReturnValidResponse()
        {
            var repo = new Mock<IOrderRepo>();
            var emailService = new Mock<IEmailService>();

            GetAllParams options = new() { Limit = 20, Offset = 0 };

            Order order1 = new() { };
            Order order2 = new() { };

            IEnumerable<Order> orders = new List<Order> { order1, order2 };
            IEnumerable<OrderReadDTO> expected = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderReadDTO>>(orders);

            repo.Setup(repo => repo.GetAllOrderAsync(options)).ReturnsAsync(orders);
            var orderService = new OrderService(repo.Object, _mapper, emailService.Object);

            var response = await orderService.GetAllOrderAsync(options);

            Assert.Equivalent(expected, response);
        }

        [Theory]
        [ClassData(typeof(SingleOrderData))]
        public async void GetOneOrderAsync_ShouldReturn_AOrder(Order? response, OrderReadDTO? result, Type? type)
        {
            var repo = new Mock<IOrderRepo>();
            var emailService = new Mock<IEmailService>();

            repo.Setup(repo => repo.GetOneOrderAsync(It.IsAny<Guid>())).ReturnsAsync(response);
            var orderService = new OrderService(repo.Object, _mapper, emailService.Object);

            if (type is not null)
            {
                await Assert.ThrowsAsync(type, () => orderService.GetOneOrderAsync(It.IsAny<Guid>()));
            }
            else
            {
                var expectedResult = await orderService.GetOneOrderAsync(It.IsAny<Guid>());
                Assert.Equivalent(result, expectedResult);
            }
        }

        public class SingleOrderData : TheoryData<Order?, OrderReadDTO?, Type?>
        {
            public SingleOrderData()
            {
                Order order = new() { };
                OrderReadDTO orderReadDTO = _mapper.Map<Order, OrderReadDTO>(order);
                Add(order, orderReadDTO, null);
                Add(null, orderReadDTO, typeof(CustomException));
            }
        }

        [Theory]
        [ClassData(typeof(CreateOrderData))]
        public async void CreateOneProductAsync_ShouldReturnValidResponse(Order order, OrderCreateDTO orderCreateDTO, OrderReadDTO expected, Type? exceptionType)
        {
            var repo = new Mock<IOrderRepo>();
            var emailService = new Mock<IEmailService>();

            repo.Setup(repo => repo.CreateOrderAsync(It.IsAny<Order>())).ReturnsAsync(order);
            var orderService = new OrderService(repo.Object, _mapper, emailService.Object);

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, () => orderService.CreateOrderAsync(orderCreateDTO));
            }
            else
            {
                var response = await orderService.CreateOrderAsync(orderCreateDTO);
                Assert.Equivalent(expected, response);
            }
        }

        public class CreateOrderData : TheoryData<Order?, OrderCreateDTO, OrderReadDTO?, Type?>
        {
            public CreateOrderData()
            {
                OrderCreateDTO orderCreateDTO = new() { OrderDetails = new List<OrderDetails> { } };

                Order order = _mapper.Map<OrderCreateDTO, Order>(orderCreateDTO);
                OrderReadDTO orderReadDTO = _mapper.Map<Order, OrderReadDTO>(order);

                Add(order, orderCreateDTO, orderReadDTO, null);
                Add(null, orderCreateDTO, null, typeof(CustomException));
            }
        }



        [Theory]
        [ClassData(typeof(UpdateOrderdata))]
        public async void UpdateOrderAsync_shouldReturnValidResponse(Order order, OrderUpdateDTO orderUpdateDTO, OrderReadDTO expected, Type? exceptionType)
        {
            var repo = new Mock<IOrderRepo>();
            var emailService = new Mock<IEmailService>();
            repo.Setup((repo) => repo.GetOneOrderAsync(It.IsAny<Guid>())).ReturnsAsync(order);
            repo.Setup((repo) => repo.UpdateOrderAsync(It.IsAny<Order>())).ReturnsAsync(order);
            var orderService = new OrderService(repo.Object, _mapper, emailService.Object);

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, () => orderService.UpdateOrderAsync(It.IsAny<Guid>(), orderUpdateDTO));
            }
            else
            {
                var result = await orderService.UpdateOrderAsync(It.IsAny<Guid>(), orderUpdateDTO);
                Assert.Equivalent(expected, result);
            }

        }
        public class UpdateOrderdata : TheoryData<Order?, OrderUpdateDTO, OrderReadDTO?, Type?>
        {
            public UpdateOrderdata()
            {
                Order order = new()
                {
                    OrderDetails = new List<OrderDetails> { }
                };
                OrderUpdateDTO orderUpdateDTO = new() { OrderStatus = OrderStatus.DELIVERED };
                Order updatedOrder = _mapper.Map(orderUpdateDTO, order);
                OrderReadDTO orderReadDTO = _mapper.Map<Order, OrderReadDTO>(updatedOrder);

                Add(order, orderUpdateDTO, orderReadDTO, null);
                Add(null, orderUpdateDTO, null, typeof(CustomException));

            }
        }






    }
}