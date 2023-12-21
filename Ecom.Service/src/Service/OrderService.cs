using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Shared;

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

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var deleteResult = await _ordeRepo.DeleteOrderAsync(orderId);
            if (!deleteResult)
            {
                throw CustomException.NotFoundException();
            }
            return deleteResult;
        }

        public async Task<IEnumerable<OrderReadDTO>> GetAllOrderAsync(GetAllParams options)
        {
            var result = await _ordeRepo.GetAllOrderAsync(options);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderReadDTO>>(result);
        }
    }
}