using helpDesk.Data;
using helpDesk.Models;

namespace helpDesk.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Comment> GetAll()
        {
            return _context.Comments.ToList();
        }

        public Comment? GetById(int id)
        {
            return _context.Comments
                .FirstOrDefault(x => x.Id == id);
        }

        public Comment Create(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }

        public Comment? Update(int id, Comment commentUpdate)
        {
            var comment = _context.Comments
                .FirstOrDefault(x => x.Id == id);

            if (comment == null)
                return null;

            comment.Content = commentUpdate.Content;

            _context.SaveChanges();

            return comment;
        }

        public Comment? Delete(int id)
        {
            var comment = _context.Comments
                .FirstOrDefault(x => x.Id == id);

            if (comment == null)
                return null;

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return comment;
        }
    }
}