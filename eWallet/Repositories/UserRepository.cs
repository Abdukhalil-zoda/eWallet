using eWallet.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace eWallet.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        public UserRepository(DataContext data)
        {
            Data = data;
        }
        public DataContext Data { get; }

        public async Task Delete(User entity)
        {
            Data.Users.Remove(entity);
            await Data.SaveChangesAsync();
        }

        public IEnumerable<User> GetAll() =>
            Data.Users;

        public User? GetById(Guid id) =>
            Data.Users.FirstOrDefault(x => x.Id == id);

        public async Task<Guid> Insert(User entity)
        {
            var id = Data.Users.Add(entity).Entity.Id;
            await Data.SaveChangesAsync();
            return id;
        }

        public async Task Update(User entity)
        {
            Data.Entry(entity).State = EntityState.Modified;
            await Data.SaveChangesAsync();
        }

        public IEnumerable<User> Where(Func<User, bool> p) =>
            Data.Users.Where(p);
    }
}
