using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
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
            GetAllParams options = new() { Limit = 20, Offset = 0 };
            var orderService = new OrderService(repo.Object, _mapper);
            await orderService.GetAllOrderAsync(options);
            repo.Verify(repo => repo.GetAllOrderAsync(options), Times.Once);
        }


        [Fact]
        public async void GetAllOrderAsync_ShouldReturnValidResponse()
        {
            var repo = new Mock<IOrderRepo>();
            GetAllParams options = new() { Limit = 20, Offset = 0 };

            Order order1 = new() { };
            Order order2 = new() { };

            IEnumerable<Order> orders = new List<Order> { order1, order2 };
            IEnumerable<OrderReadDTO> expected = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderReadDTO>>(orders);

            repo.Setup(repo => repo.GetAllOrderAsync(options)).ReturnsAsync(orders);
            var orderService = new OrderService(repo.Object, _mapper);
            var response = await orderService.GetAllOrderAsync(options);

            Assert.Equivalent(expected, response);
        }

        [Theory]
        [ClassData(typeof(SingleOrderData))]
        public async void GetOneOrderAsync_ShouldReturn_AOrder(Order? response, OrderReadDTO? result, Type? type)
        {
            var repo = new Mock<IOrderRepo>();
            repo.Setup(repo => repo.GetOneOrderAsync(It.IsAny<Guid>())).ReturnsAsync(response);
            var orderService = new OrderService(repo.Object, _mapper);

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
            repo.Setup(repo => repo.CreateOrderAsync(It.IsAny<Order>())).ReturnsAsync(order);
            var orderService = new OrderService(repo.Object, _mapper);

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


    }
}