using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Models.User;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FoodDelivery.Services;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private AppSettings _appSettings;

        public AccountController(IAccountService accountService, AppSettings appSettings)
        {
            _accountService = accountService;
            _appSettings = appSettings;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {
            try
            {
                return Ok(await _accountService.RegisterUser(user));
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
       
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCredentials credentials)
        {
            try
            {
                return Ok(await _accountService.Login(credentials));
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
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            Guid userId;
            if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value == null)
            {
                return StatusCode(_appSettings.Exeptions[2].Code, new Response(_appSettings.Exeptions[2].Message));
                //return BadRequest(new Response("Jwt token does not contain id"))
            };
            userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                return Ok(await _accountService.GetProfile(userId));
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
        [HttpPut("Profile")]
        public async Task<IActionResult> EditProfile(UserEditModel editModel)
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
                await _accountService.EditProfile(userId, editModel);
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
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            string? jwtToken = HttpContext.Request.Headers["Authorization"];
            if (jwtToken != null)
            {
                jwtToken = jwtToken.Substring("Bearer ".Length);
                try
                {
                    await _accountService.BanToken(jwtToken);
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
            else
            { 
                return BadRequest();
            }
        }
    }
}
