using DSCC.CW1._9713.API.Models;

namespace DSCC.CW1._9713.API.Services
{
    public interface IService<T>
    {
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int Id);
        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
