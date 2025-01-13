using Microsoft.EntityFrameworkCore;
using StayNest_API.Data.Models;
using StayNest_API.Data;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using StayNest_API.Interfaces;

namespace StayNest_API.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;

        public UserService(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public async Task<List<Users>> GetAllUserAsync()
        {
            return await _databaseContext.Users.ToListAsync();
        }

        public string GenerateToken(Users users)
        {
            if (users == null)
                throw new ArgumentNullException(nameof(users), "User cannot be null");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Auth:Secret"]);

            //var roles = users.Roles?.Select(r => r.Name) ?? new List<string> { "Prijavljeni korisnik" };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", users.Id.ToString()),
                new Claim("role", users.Roles)


            };

            //claims.AddRange(roles.Select(role => new Claim("role", role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }


        public string HashPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return String.Empty;
            }

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
        public async Task AddUserToRole(Users users)
        {
            await _databaseContext.Users.AddAsync(users);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task RegisterUser(Users users)
        {
            await _databaseContext.Users.AddAsync(users);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task CreateRole(UserRole role)
        { 
            await _databaseContext.UserRole.AddAsync(role);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<Users?> GetUserByUsername(string username) =>
            await _databaseContext.Users.FirstOrDefaultAsync(x => x.Username == username);

        public async Task DeleteUser(int userId)
        {
            var user = await _databaseContext.Users.FindAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("Korisnik nije pronađen.");

            _databaseContext.Users.Remove(user);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<string> GetUserRole(int userId)
        {
            var userRole = await _databaseContext.UserRole
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Name) 
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(userRole))
            {
                throw new Exception("Korisnik nema dodeljenu ulogu.");
            }

            return userRole;
        }


    }
} 
