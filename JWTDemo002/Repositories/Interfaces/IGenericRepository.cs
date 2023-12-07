namespace JWTDemo002.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> GetAsync(Guid id);
        Task<T> InsertAsync(T entity);
    }
}
