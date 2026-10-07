using helpDesk.Models;

namespace helpDesk.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(int id);
        User? Create(User user);
        User? Update(int id, User userUpdate);
        User? Delete(int id);
    }
}