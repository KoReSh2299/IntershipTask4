using IntershipTask4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Domain.abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get(bool trackChanges);
        Task<User?> Get(int id, bool trackChanges);
        Task Create(User entity);
        bool Delete(int Id);
        void Update(User entity);
        Task SaveChangesAsync();
    }
}
