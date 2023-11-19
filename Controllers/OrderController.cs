using FoodDelivery.Models;
using FoodDelivery.Models.Order;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private AppSettings _appSettings;

        public OrderController(IOrderService orderService, AppSettings appSettings)
        {
            _orderService = orderService;
            _appSettings = appSettings;
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            try
            {
                return Ok(await _orderService.GetOrder(orderId));
            }
            catch (ExceptionWithStatusCode ex)
            {
                return StatusCode(ex.StatusCode, new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(ex.Message));
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrderList()
        {
            Guid userId;
            if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value == null)
            {
                return StatusCode(_appSettings.Exeptions[2].Code, new Response(_appSettings.Exeptions[2].Message));
                //return BadRequest(new Response("Jwt token does not contain id"))
            }
            userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                return Ok(await _orderService.GetOrderList(userId));
            }
            catch (ExceptionWithStatusCode ex)
            {
                return StatusCode(ex.StatusCode, new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(ex.Message));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDTO orderCreateDTO)
        {

            Guid userId;
            if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value == null)
            {
                return StatusCode(_appSettings.Exeptions[2].Code, new Response(_appSettings.Exeptions[2].Message));
                //return BadRequest(new Response("Jwt token does not contain id"))
            }
            userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                await _orderService.CreateOrder(userId, orderCreateDTO);
                return Ok();
            }
            catch (ExceptionWithStatusCode ex)
            {
                return StatusCode(ex.StatusCode, new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(ex.Message));
            }
        }

        [Authorize]
        [HttpPost("{orderId}/Status")]
        public async Task<IActionResult> ConfirmOrderDelivery(Guid orderId)
        {
            try
            {
                await _orderService.ConfirmOrderDelivery(orderId);
                return Ok();
            }
            catch (ExceptionWithStatusCode ex)
            {
                return StatusCode(ex.StatusCode, new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(ex.Message));
            }
        }
    }
}
