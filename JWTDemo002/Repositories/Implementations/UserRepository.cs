using JWTDemo002.Model;
using JWTDemo002.Repositories.Interfaces;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JWTDemo002.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        #region Read and Write methods
        private static string Read(string fileName)
        {
            try
            {
                var currDirectory = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(currDirectory, fileName);
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static bool Write(string fileName, string content)
        {
            try
            {
                var currDirectory = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(currDirectory, fileName);
                File.WriteAllText(filePath, content);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        public async Task<User?> GetAsync(Guid id)
        {
            try
            {
                #region reading data
                var data = JsonConvert.DeserializeObject<IEnumerable<User>>(
                Read("users.json")
                );
                #endregion

                return data.FirstOrDefault(d => d.Id == id);
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<User>?> GetAllAsync()
        {
            #region reading data
            var data = JsonConvert.DeserializeObject<IEnumerable<User>>(
            Read("users.json")
            );
            #endregion

            return data;
        }

        public async Task<User> InsertAsync(User entity)
        {
            try
            {
                #region reading data
                var data = JsonConvert.DeserializeObject<IEnumerable<User>>(
                Read("users.json")
                )?.ToList();
                #endregion
                #region writing data
                if (data == null) data = new();
                data.Add(entity);
                var newData = JsonConvert.SerializeObject(data);
                Write("users.json", newData);
                #endregion

                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User?> GetByName(string username)
        {
            try
            {
                var data = await GetAllAsync();
                return data?.FirstOrDefault(x => x.Username == username);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
