using helpDesk.Dtos;
using helpDesk.Enums;
using helpDesk.Exceptions;
using helpDesk.Models;
using helpDesk.Repositories;

namespace helpDesk.Services
{
    public class TicketsServices
    {
        private readonly ITicketRepository _repository;
        private readonly IUserRepository _userRepository;

        public TicketsServices(ITicketRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public List<TicketDto> GetAll()
        {
            var tickets = _repository.GetAll();
            return tickets.Select(ToDto).ToList();
        }

        public TicketDto GetById(int id)
        {
            var ticket = _repository.GetById(id);
            if (ticket == null)
                throw new NotFoundException("Id do chamado não encontrado.");
            return ToDto(ticket);
        }

        public TicketDto Create(CreateTicketDto dto)
        {
            var user = _userRepository.GetById(dto.CustomerId);
            if (user == null)
                throw new NotFoundException("Id do usuário não encontrado.");

            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                CustomerId = dto.CustomerId
            };
            return ToDto(_repository.Create(ticket));
        }

        public TicketDto Update(int id, UpdateTicketDto dto)
        {
            var ticketUpdate = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority
            };
            var ticket = _repository.Update(id, ticketUpdate);
            if (ticket == null)
                throw new NotFoundException("Id do chamado não encontrado.");
            return ToDto(ticket);
        }

        public TicketDto Delete(int id)
        {
            var ticket = _repository.Delete(id);
            if (ticket == null)
                throw new NotFoundException("Id do chamado não encontrado.");
            return ToDto(ticket);
        }

        public TicketDto Accept(int id, int userId)
        {
            var ticket = _repository.GetById(id);
            if (ticket == null)
                throw new NotFoundException("Id do chamado não encontrado.");
            if (ticket.Status != TicketStatus.Open)
                throw new InvalidOperationException("Somente chamados abertos podem ser aceitos.");

            ticket.Status = TicketStatus.InProgress;
            ticket.AssignedUserId = userId;
            return ToDto(_repository.Update(id, ticket)!);
        }

        public TicketDto Close(int id, int userId, string role)
        {
            var ticket = _repository.GetById(id);
            if (ticket == null)
                throw new NotFoundException("Id do chamado não encontrado.");
            if (role != nameof(UserRole.Admin) && ticket.AssignedUserId != userId)
                throw new UnauthorizedAccessException("Somente o agente responsável ou um administrador pode fechar o chamado.");
            if (ticket.Status != TicketStatus.InProgress)
                throw new InvalidOperationException("Somente chamados em andamento podem ser fechados.");

            ticket.Status = TicketStatus.Closed;
            ticket.ClosedAt = DateTime.UtcNow;
            return ToDto(_repository.Update(id, ticket)!);
        }

        private static TicketDto ToDto(Ticket ticket) => new()
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            Priority = ticket.Priority,
            CreatedAt = ticket.CreatedAt,
            ClosedAt = ticket.ClosedAt,
            CustomerId = ticket.CustomerId,
            AssignedUserId = ticket.AssignedUserId
        };
    }
}
