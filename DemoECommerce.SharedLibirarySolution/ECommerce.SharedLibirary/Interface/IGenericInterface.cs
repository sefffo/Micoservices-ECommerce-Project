using System.Linq.Expressions;
using ECommerce.SharedLibirary.Responses;

namespace ECommerce.SharedLibirary.Interface
{
    public interface IGenericInterface<T>  where T : class
    {
        Task<Response> CreateAsync(T Entity);
        Task<Response> UpdateAsync(T Entity);
        Task<Response> DeleteAsync(T Entity);
        Task<T> FindByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        //Task<Response> UpdateAsync(int Id, T Entity);



        Task<T> GetBy(Expression<Func<T, bool>> Predicate);

    }

    
}
