using FoodDelivery.Models;
using FoodDelivery.Models.User;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IAddressService _addressService;

        public AddressController (IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] int parentObjectid, [FromQuery] string? query = null)
        {
            try
            {
                return Ok(await _addressService.Search(parentObjectid, query));
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

        [HttpGet("Chain")]
        public async Task<IActionResult> GetChain([FromQuery] Guid bjectGuid)
        {
            try
            {
                return Ok(await _addressService.GetChain(bjectGuid));
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
