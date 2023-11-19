using FoodDelivery.Models;
using FoodDelivery.Models.Basket;
using FoodDelivery.Models.Dish;
using FoodDelivery.Models.Order;
using Microsoft.EntityFrameworkCore;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Guid userId, OrderCreateDTO orderCreateDTO);
        Task<OrderDTO> GetOrder(Guid orderId);
        Task<List<OrderInfoDTO>> GetOrderList(Guid userId);
        Task ConfirmOrderDelivery(Guid orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly FDDbContext _context;
        private readonly IBasketService _basketService;
        private readonly AppSettings _appSettings;


        public OrderService(FDDbContext context, IBasketService basketService, AppSettings appSettings)
        {
            _context = context;
            _basketService = basketService;
            _appSettings = appSettings;
        }

        public async Task ConfirmOrderDelivery(Guid orderId)
        {
            OrderModel? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                if (order.Status == OrderStatus.Delivered)
                {
                    //throw new ExceptionWithStatusCode(400, "Order is already confirmed");
                    throw new ExceptionWithStatusCode(_appSettings.Exeptions[8].Code, _appSettings.Exeptions[8].Message);
                }
                order.Status = OrderStatus.Delivered;
                await _context.SaveChangesAsync();
            }
            else
            {
                //throw new ExceptionWithStatusCode(404, "Order not found");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[9].Code, _appSettings.Exeptions[9].Message);
            }
        }

        public async Task<List<OrderInfoDTO>> GetOrderList(Guid userId)
        {
            List<OrderModel> orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            List<OrderInfoDTO> orderInfos = new List<OrderInfoDTO>();
            foreach (var order in orders)
            {
                List<DishBasketDTO> dishes = await GetDishesInOrder(order.Id);
                double price = 0;
                foreach (var dish in dishes)
                {
                    price += dish.TotalPrice;
                }

                orderInfos.Add(new OrderInfoDTO(order, price));
            }

            return orderInfos;
        }

        public async Task<OrderDTO> GetOrder(Guid orderId)
        {
            OrderModel? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                List<DishBasketDTO> dishes = await GetDishesInOrder(orderId);
                double price = 0;
                foreach (var dish in dishes)
                {
                    price += dish.TotalPrice;
                }
                return new OrderDTO(order, dishes, price);
            }
            else
            {
                //throw new ExceptionWithStatusCode(404, "Order not found");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[9].Code, _appSettings.Exeptions[9].Message);
            }
        }

        private async Task<List<DishBasketDTO>> GetDishesInOrder(Guid orderId)
        {
            List<OrderToDishesModel> dishesInOrder = await _context.OrderToDishes.Where(o => o.OrderId == orderId).ToListAsync();
            List<DishBasketDTO> dishBasketDTOs = new List<DishBasketDTO>();

            foreach (var item in dishesInOrder)
            {
                DishModel? dish = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == item.DishId);
                if (dish == null)
                {
                    //throw new ExceptionWithStatusCode(404, "Dish not found");
                    throw new ExceptionWithStatusCode(_appSettings.Exeptions[4].Code, _appSettings.Exeptions[4].Message);
                }
                dishBasketDTOs.Add(new DishBasketDTO(dish, item.Count));
            }

            return dishBasketDTOs;
        }

        public async Task CreateOrder(Guid userId, OrderCreateDTO orderCreateDTO)
        {
            List<DishBasketDTO> dishesInBasket = await _basketService.GetBasket(userId);
            if (dishesInBasket.Count == 0)
            {
                //throw new ExceptionWithStatusCode(400, "Basket is empty");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[10].Code, _appSettings.Exeptions[10].Message);
            }

            TimeSpan timeDifference = orderCreateDTO.DeliveryTime.Subtract(DateTime.Now);
            if (timeDifference.TotalMinutes < _appSettings.MinOrderTimeInMinuts)
            {
                //throw new ExceptionWithStatusCode(400, "Delivery time is too short");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[11].Code, _appSettings.Exeptions[11].Message);
            }

            OrderModel orderModel = new OrderModel(userId, DateTime.Now, orderCreateDTO.DeliveryTime, orderCreateDTO.AddressId);
            await _context.Orders.AddAsync(orderModel);

            foreach (var item in dishesInBasket)
            {
                await _context.OrderToDishes.AddAsync(new OrderToDishesModel(orderModel.Id, item.Id, item.Count));
            }

            await _context.SaveChangesAsync();

            await _basketService.ClearBusket(userId);
        }
    }
}
