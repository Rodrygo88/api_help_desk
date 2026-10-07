using helpDesk.Data;
using helpDesk.Models;

namespace helpDesk.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User? Update(int id, User userUpdate)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return null;

            user.Name = userUpdate.Name;
            user.Email = userUpdate.Email;
            user.PasswordHash = userUpdate.PasswordHash;
            user.Role = userUpdate.Role;

            _context.SaveChanges();

            return user;
        }

        public User? Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return null;

            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}