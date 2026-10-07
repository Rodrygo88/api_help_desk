using helpDesk.Dtos;
using helpDesk.Exceptions;
using helpDesk.Models;
using helpDesk.Repositories;

namespace helpDesk.Services
{
    public class CommentsServices
    {
        private readonly ICommentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public CommentsServices(
            ICommentRepository repository,
            IUserRepository userRepository,
            ITicketRepository ticketRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        public List<Comment> GetAll()
        {
            return _repository.GetAll();
        }

        public CommentDto GetById(int id)
        {
            var comment = _repository.GetById(id);

            if (comment == null)
            {
                throw new NotFoundException("Id do comentário não encontrado.");
            }

            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                TicketId = comment.TicketId,
                UserId = comment.UserId
            };
        }

        public CommentDto Create(CreateCommentDto dto)
        {
            var user = _userRepository.GetById(dto.UserId);

            if (user == null)
            {
                throw new NotFoundException("Id do usuário não encontrado.");
            }

            var ticket = _ticketRepository.GetById(dto.TicketId);

            if (ticket == null)
            {
                throw new NotFoundException("Id do chamado não encontrado.");
            }

            var comment = new Comment
            {
                Content = dto.Content,
                UserId = dto.UserId,
                TicketId = dto.TicketId
            };

            var createdComment = _repository.Create(comment);

            return new CommentDto
            {
                Id = createdComment.Id,
                Content = createdComment.Content,
                CreatedAt = createdComment.CreatedAt,
                TicketId = createdComment.TicketId,
                UserId = createdComment.UserId
            };
        }

        public CommentDto Update(int id, UpdateCommentDto dto)
        {
            var commentUpdate = new Comment
            {
                Content = dto.Content
            };

            var comment = _repository.Update(id, commentUpdate);

            if (comment == null)
            {
                throw new NotFoundException("Id do comentário não encontrado.");
            }

            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                TicketId = comment.TicketId,
                UserId = comment.UserId
            };
        }

        public CommentDto Delete(int id)
        {
            var comment = _repository.Delete(id);

            if (comment == null)
            {
                throw new NotFoundException("Id do comentário não encontrado.");
            }

            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                TicketId = comment.TicketId,
                UserId = comment.UserId
            };
        }
    }
}