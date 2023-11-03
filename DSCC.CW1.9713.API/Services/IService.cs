using DSCC.CW1._9713.API.Models;

namespace DSCC.CW1._9713.API.Services
{
    public interface IService<T>
    {
        void Create(T item);
        void Update(T item);
        void Delete(int Id);
        T? GetById(int Id);
        IEnumerable<T> GetAll();
    }
}
