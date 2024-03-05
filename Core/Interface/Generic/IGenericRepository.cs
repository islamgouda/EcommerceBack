using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IGenericRepository<T> where T :BaseEntity
    {
        Task<T>GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T>GetEntityWithSpecification(ISpecification<T> specifications);
        Task<IReadOnlyList<T>>ListAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
