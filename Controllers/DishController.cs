using FoodDelivery.Models;
using FoodDelivery.Models.User;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishService _dishService;
        private AppSettings _appSettings;

        public DishController(IDishService dishService, AppSettings appSettings)
        {
            _dishService = dishService;
            _appSettings = appSettings;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishById(Guid id)
        {
            try
            {
                return Ok(await _dishService.GetDishById(id));
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

        [HttpGet]
        public async Task<IActionResult> GetDishList(
            [FromQuery(Name = "categories")] DishCategory[] categories,
            [FromQuery] bool? vegetarian = null,
            [FromQuery] SortingTypes? sorting = null,
            [FromQuery] int pageSize = 5,
            [FromQuery] int page = 1)
        {
            try
            {
                return Ok(await _dishService.GetDishPagedList(categories, vegetarian, sorting, pageSize, page));
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
        [HttpPost("{dishId}/Raiting")]
        public async Task<IActionResult> RateDish(Guid dishId, [FromQuery] int ratingScore = 0)
        {
            if (ratingScore < 1 || ratingScore > 5)
            {
                return BadRequest(new Response("Invalid rating score. It should be in the range of 1 to 5."));
            }

            Guid userId;
            if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value == null)
            {
                return StatusCode(_appSettings.Exeptions[2].Code, new Response(_appSettings.Exeptions[2].Message));
                //return BadRequest(new Response("Jwt token does not contain id"))
            }
            userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                await _dishService.RateDish(dishId, userId, ratingScore);
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
        [HttpGet("{dishId}/Raiting/Check")]
        public async Task<IActionResult> ChekAbleToRating(Guid dishId)
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
                return Ok(await _dishService.ChekDishAbleToRaiting(dishId, userId));
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
