using JWTDemo002.Model;

namespace JWTDemo002.Repositories.Interfaces
{
    public interface IUserService : IGenericService<UserDto>
    {
        Task<ApiResponse> LoginAsync(UserDto request);
        Task<ApiResponse> InsertAsync(UserDto request);
    }
}
