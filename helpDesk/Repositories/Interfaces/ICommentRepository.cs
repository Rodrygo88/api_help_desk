using helpDesk.Models;

namespace helpDesk.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAll();
        Comment? GetById(int id);
        Comment Create(Comment comment);
        Comment? Update(int id, Comment commentUpdate);
        Comment? Delete(int id);
    }
}