using StayNest_API.Data.Models;

namespace StayNest_API.Interfaces
{
    public class IUserService
    {
        Task<List<User>> GetAllUsersAsync();
    }
}
