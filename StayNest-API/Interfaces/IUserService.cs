using StayNest_API.Data.Models;

namespace StayNest_API.Interfaces
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUserAsync();
        string GenerateToken(Users user);
        string HashPassword(string password);
        Task AddUserToRole(Users user);
        Task RegisterUser(Users user);
        Task CreateRole(UserRole role);
        Task<Users?> GetUserByUsername(string username);
    }
}
