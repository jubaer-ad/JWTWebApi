using JWTDemo002.Model;

namespace JWTDemo002.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByName(string username);
    }
}
