using helpDesk.Dtos;
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

            var result = tickets.Select(ticket => new TicketDto
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
            }).ToList();

            return result;
        }

        public TicketDto GetById(int id)
        {
            var ticket = _repository.GetById(id);

            if (ticket == null)
            {
                throw new NotFoundException("Id do chamado não encontrado.");
            }

            var result = new TicketDto
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

            return result;
        }

        public TicketDto Create(CreateTicketDto dto)
        {
            var user = _userRepository.GetById(dto.CustomerId);
            if (user == null)
            {
                throw new NotFoundException("Id do usuário não encontrado.");
            }
                
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                CustomerId = dto.CustomerId
            };

            var createdTicket = _repository.Create(ticket);

            var result = new TicketDto
            {
                Id = createdTicket.Id,
                Title = createdTicket.Title,
                Description = createdTicket.Description,
                Status = createdTicket.Status,
                Priority = createdTicket.Priority,
                CreatedAt = createdTicket.CreatedAt,
                ClosedAt = createdTicket.ClosedAt,
                CustomerId = createdTicket.CustomerId,
                AssignedUserId = createdTicket.AssignedUserId
            };

            return result;
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
            {
                throw new NotFoundException("Id do chamado não encontrado.");
            }

            var result = new TicketDto
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

            return result;
        }

        public TicketDto Delete(int id)
        {
            var ticket = _repository.Delete(id);
            if (ticket == null)
            {
                throw new NotFoundException("Id do chamado não encontrado.");
            }

            var result = new TicketDto
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

            return result;
        }


    }


}
