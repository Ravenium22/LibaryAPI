namespace Kutuphane.Repositories.Interfaces
{
    public interface IRepository<T> where T : class // T=Type bu sayede int,string olması imkansız
    {
        Task<IEnumerable<T>> GetAllAsync(); // Liste gibi
        Task<T?> GetByIdAsync(int id);  //t? // null olabilir
        Task<T> AddAsync(T entity); 
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}