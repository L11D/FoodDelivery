using FoodDelivery.Models;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketService _basketService;
        private AppSettings _appSettings;


        public BasketController(IBasketService basketService, AppSettings appSettings)
        {
            _basketService = basketService;
            _appSettings = appSettings;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBsket()
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
                return Ok(await _basketService.GetBasket(userId));
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
        [HttpPost("Dish/{dishId}")]
        public async Task<IActionResult> AddDish(Guid dishId)
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
                await _basketService.AddDish(dishId, userId);
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
        [HttpDelete("Dish/{dishId}")]
        public async Task<IActionResult> DeleteDish(Guid dishId, [FromQuery] bool increase = false)
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
                await _basketService.DeleteDish(dishId, userId, increase);
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
