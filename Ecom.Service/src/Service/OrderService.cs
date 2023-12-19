using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;

namespace Ecom.Service.src.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _ordeRepo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepo orderRepo, IMapper mapper)
        {
            _ordeRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<OrderReadDTO> CreateOrderAsync(OrderCreateDTO orderCreateDTO)
        {
            var order = _mapper.Map<OrderCreateDTO, Order>(orderCreateDTO);
            var result = await _ordeRepo.CreateOrderAsync(order);
            return _mapper.Map<Order, OrderReadDTO>(result);

        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync(GetAllParams options)
        {
            var result = await _ordeRepo.GetAllOrderAsync(options);

            return result;
        }
    }
}