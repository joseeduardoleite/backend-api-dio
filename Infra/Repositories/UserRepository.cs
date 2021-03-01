using System.Linq;
using Business.Entities;
using Business.Services;
using Infra.Data;

namespace Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(User user) => _context.Add(user);

        public void Commit() => _context.SaveChanges();

        public User GetUser(string login) => _context.Users.FirstOrDefault(x => x.Login == login);
    }
}