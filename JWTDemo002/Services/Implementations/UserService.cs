using JWTDemo002.Model;
using JWTDemo002.Repositories.Interfaces;
using System.Net;

namespace JWTDemo002.Repositories.Implementations
{
    public class UserService(
        IUserRepository userRepository,
        IConfiguration configuration
        ) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<UserDto?> GetAsync(Guid id)
        {
            try
            {
                var res = await _userRepository.GetAsync(id);
                if (res == null)
                {
                    return null;
                }
                return new()
                {
                    Username = res.Username,
                    Password = res.Passwordhash
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UserDto>?> GetAllAsync()
        {
            try
            {
                var res = await _userRepository.GetAllAsync();
                var data = new List<UserDto>();
                foreach(var r in res)
                {
                    data.Add(new UserDto
                    {
                        Password = r.Passwordhash,
                        Username = r.Username
                    });
                }
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse> LoginAsync(UserDto request)
        {
            try
            {
                var res = await _userRepository.GetAllAsync();
                var targetUser = res.FirstOrDefault(x => x.Username == request.Username);
                if (targetUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, targetUser.Passwordhash))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Username or password is incorrect!",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }
                var jwtSecretKey = _configuration.GetSection("JWTKey").Value!;
                var jwtToken = Helper.Helper.JWTToken(targetUser, jwtSecretKey);
                LoginRsp loginRsp = new()
                {
                    BearerToken = jwtToken,
                    User = targetUser
                };
                return new()
                {
                    IsSuccess = true,
                    Message = "Login success",
                    StatusCode = HttpStatusCode.OK,
                    Data = loginRsp
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse> InsertAsync(UserDto entity)
        {
            try
            {
                var existingData = await _userRepository.GetByName(entity.Username);
                if(existingData != null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Username already exists",
                        StatusCode = HttpStatusCode.Conflict
                    };
                }
                User data = new()
                {
                    Id = Guid.NewGuid(),
                    Passwordhash = BCrypt.Net.BCrypt.HashPassword(entity.Password),
                    Username = entity.Username
                };
                var res = await _userRepository.InsertAsync(data);
                return new()
                {
                    IsSuccess = true,
                    Message = "User addd successfully",
                    StatusCode = HttpStatusCode.OK,
                    Data = res
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
