using FoodDelivery.Models;
using FoodDelivery.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Services
{
    public interface IAccountService
    {
        Task<TokenResponse> RegisterUser(UserRegisterModel model);
        Task<TokenResponse> Login(LoginCredentials credentials);
        Task<UserDTO> GetProfile(Guid userId);
        Task EditProfile(Guid userId, UserEditModel model);
        Task BanToken(string token);
    }
    public class AccountService : IAccountService
    {
        private readonly FDDbContext _context;
        private readonly AppSettings _appSettings;

        public AccountService(FDDbContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        public async Task<TokenResponse> RegisterUser(UserRegisterModel model)
        {
            if (await UserExist(model))
            {
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[0].Code, _appSettings.Exeptions[0].Message);
                //throw new ExceptionWithStatusCode(400, "Email is already in use");
            }

            await Add(new UserModel(
            model.FullName,
            model.Email,
            model.Password,
            model.BirthDate,
            model.Gender,
            model.PhoneNumber,
            model.AddressId));

            return await Login(new LoginCredentials(model.Email, model.Password));
        }

        private async Task Add(UserModel model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> UserExist(UserRegisterModel model)
        {
            return await _context.Users.AnyAsync(u => u.Email == model.Email);
        }

        public async Task<TokenResponse> Login(LoginCredentials credentials)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == credentials.Email && u.Password == credentials.Password);
            if (user != null)
            {
                return GetToken(user);
            }
            else
            {
                //throw new ExceptionWithStatusCode(404, "User not found");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[1].Code, _appSettings.Exeptions[1].Message);
            }
        }

        private TokenResponse GetToken(UserModel user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_appSettings.JwtKeyLifeTime)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtKey)), SecurityAlgorithms.HmacSha256));

            return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        public async Task<UserDTO> GetProfile(Guid userId)
        {

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                return new UserDTO(user.Id, user.FullName, user.Email, user.BirthDate, user.Gender, user.PhoneNumber, user.AddressId);
            }
            else
            {
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[1].Code, _appSettings.Exeptions[1].Message);
                //throw new ExceptionWithStatusCode(404, "User with id not found");
            }
        }

        public async Task EditProfile(Guid userId, UserEditModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.FullName = model.FullName;
                user.BirthDate = model.BirthDate;
                user.Gender = model.Gender;
                user.PhoneNumber = model.PhoneNumber;
                user.AddressId = model.AddressId;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[1].Code, _appSettings.Exeptions[1].Message);
                //throw new ExceptionWithStatusCode(404, "User with id not found");
            }
        }

        public async Task BanToken(string token)
        {
            BannedToken tonken = new BannedToken(token);
            await _context.BannedTokens.AddAsync(tonken);
            await _context.SaveChangesAsync();
        }
    }
}
