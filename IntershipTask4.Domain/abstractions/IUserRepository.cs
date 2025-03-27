using IntershipTask4.Domain.Entities;
using IntershipTask4.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Domain.abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get(Specification<User> specification, bool trackChanges);
        Task<User?> Get(int id, Specification<User> specification, bool trackChanges);
        Task<User?> GetByEmail(string email, Specification<User> specification, bool trackChanges);
        Task Create(User entity);
        bool Delete(int Id);
        void Update(User entity);
        Task SaveChangesAsync();
    }
}
