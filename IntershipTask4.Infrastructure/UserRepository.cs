using IntershipTask4.Domain.abstractions;
using IntershipTask4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTask4.Infrastructure
{
    public class UserRepository(IntershipTask4Context context) : IUserRepository
    {
        private readonly IntershipTask4Context _dbContext = context;

        public async Task Create(User entity) => await _dbContext.Users.AddAsync(entity);
        public bool Delete(int Id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == Id);
            if(user != null && user.DeletedAt == null)
            {
                user.DeletedAt = DateTime.Now;
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<User>> Get(bool trackChanges) => 
            await (!trackChanges 
                ? _dbContext.Users.AsNoTracking()
                : _dbContext.Users).ToListAsync();

        public async Task<User?> Get(int id, bool trackChanges) =>
            await (!trackChanges
                ? _dbContext.Users.AsNoTracking()
                : _dbContext.Users).FirstOrDefaultAsync(u => u.Id == id);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public void Update(User entity) => _dbContext.Users.Update(entity);
    }
}
