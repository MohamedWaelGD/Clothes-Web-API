using BloggerAPIApp.Services.ImagesServices;
using Clothes_Web_API_App.Data;
using Clothes_Web_API_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Clothes_Web_API_App.Repository.UserRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private const string REFRESH_TOKEN = "refreshtoken";

        private readonly AppDbContext _appDbContext;
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(
            AppDbContext appDbContext, 
            IImageService imageService,
            IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _imageService = imageService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseApi<User>> Create(User user, string password, IFormFile profilePicture)
        {
            if (await IsEmailExists(user.Email))
            {
                return ResponseApi<User>.GenerateErrorMessage("Email found");
            }

            user.ProfilePicturePath = _configuration.GetSection("MainURL").Value + "/images/Unknown_person.jpg";

            if (profilePicture != null)
                user.ProfilePicturePath = await _imageService.UploadImage(profilePicture);

            GenerateHashPassword(password, out var ph, out var ps);

            user.PasswordHash = ph;
            user.PasswordSalt = ps;

            await Create(user);

            return ResponseApi<User>.GenerateSuccessMessage("Successfully response registered", user);
        }

        #region Create And Update User
        public async Task<ResponseApi<User>> Update(User user, string password, IFormFile profilePicture)
        {
            if (!await IsEmailExists(user.Email))
            {
                return ResponseApi<User>.GenerateErrorMessage("Email not found");
            }

            if (profilePicture != null)
                user.ProfilePicturePath = await _imageService.UploadImage(profilePicture);

            if (!VerifyHashPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                GenerateHashPassword(password, out var ph, out var ps);
                user.PasswordHash = ph;
                user.PasswordSalt = ps;
            }

            await Update(user);

            return ResponseApi<User>.GenerateSuccessMessage("Successfully response registered", user);
        }


        public async Task<ResponseApi<User>> GetAuthorizedUser()
        {
            var user = await FindByCondition(e => e.Id == GetAuthorizedUserId()).FirstOrDefaultAsync();

            if (user == null)
            {
                return ResponseApi<User>.GenerateErrorMessage("User not authorized");
            }

            return ResponseApi<User>.GenerateSuccessMessage("User not authorized", user);
        }
        #endregion

        public int GetAuthorizedUserId()
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return (id == null) ? -1 : int.Parse(id);
        }

        public async Task<ResponseApi<string>> Login(string email, string password)
        {
            if (!await IsEmailExists(email))
            {
                return ResponseApi<string>.GenerateErrorMessage("Email not found");
            }

            var user = await FindByCondition(e => e.Email == email).FirstOrDefaultAsync();

            if (!VerifyHashPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return ResponseApi<string>.GenerateErrorMessage("Password is not valid");
            }

            return ResponseApi<string>.GenerateSuccessMessage("Successfully logged in", GenerateToken(user));
        }
        public async Task<ResponseApi<string>> RefreshToken()
        {
            var response = await GetAuthorizedUser();

            if (!response.Success)
                return ResponseApi<string>.GenerateErrorMessage("Not authorized");

            var user = response.Data;

            var refreshToken = _httpContextAccessor.
                HttpContext?.
                Request.
                Cookies[REFRESH_TOKEN];

            if (!user.RefreshToken.Equals(refreshToken))
                return ResponseApi<string>.GenerateErrorMessage("Not authorized");

            if (user.TokenExpires < DateTime.Now)
                return ResponseApi<string>.GenerateErrorMessage("Date expired.");

            string token = GenerateToken(user);
            await GenerateRefreshToken(user);
            
            return ResponseApi<string>.GenerateSuccessMessage("Token generated", token);
        }

        private async Task GenerateRefreshToken(User user)
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var created = DateTime.Now;
            var expires = DateTime.Now.AddDays(7);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires
            };

            _httpContextAccessor.
                HttpContext?.
                Response.
                Cookies.
                Append(REFRESH_TOKEN, refreshToken, cookieOptions);

            user.RefreshToken = refreshToken;
            user.TokenCreated = created;
            user.TokenExpires = expires;

            await Update(user);
        }

        public async Task<bool> IsUserExists(int id)
        {
            return await _appDbContext.Users.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> IsEmailExists(string email)
        {
            return await _appDbContext.Users.AnyAsync(e => e.Email == email);
        }

        private void GenerateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyHashPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash.SequenceEqual(computedHash);
            }
        }

        private string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
