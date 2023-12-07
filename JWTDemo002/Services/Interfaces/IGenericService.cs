namespace JWTDemo002.Repositories.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> GetAsync(Guid id);
    }
}
