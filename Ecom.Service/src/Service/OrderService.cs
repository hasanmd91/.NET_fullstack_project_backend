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
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepo orderRepo, IMapper mapper, IEmailService emailService)
        {
            _ordeRepo = orderRepo;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<OrderReadDTO> CreateOrderAsync(OrderCreateDTO orderCreateDTO)
        {
            var order = _mapper.Map<OrderCreateDTO, Order>(orderCreateDTO);
            var result = await _ordeRepo.CreateOrderAsync(order) ?? throw CustomException.BadRequestException();
            await _emailService.SendOrderConfirmationEmailAsync(result);

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

        public async Task<OrderReadDTO> GetOneOrderAsync(Guid orderId)
        {
            var result = await _ordeRepo.GetOneOrderAsync(orderId) ?? throw CustomException.NotFoundException("Order is not found");
            return _mapper.Map<Order, OrderReadDTO>(result);
        }

        public async Task<OrderReadDTO> UpdateOrderAsync(Guid orderId, OrderUpdateDTO orderUpdateDTO)
        {
            var order = await _ordeRepo.GetOneOrderAsync(orderId) ?? throw CustomException.NotFoundException("Order is not found");
            var updatedOrder = _mapper.Map(orderUpdateDTO, order);

            var result = await _ordeRepo.UpdateOrderAsync(updatedOrder);
            return _mapper.Map<Order, OrderReadDTO>(result);
        }
    }
}