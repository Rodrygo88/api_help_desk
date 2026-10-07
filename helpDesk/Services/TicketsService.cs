using helpDesk.Data;
using helpDesk.Dtos;
using helpDesk.Exceptions;
using helpDesk.Models;

namespace helpDesk.Services
{
    public class TicketsServices
    {
        private readonly AppDbContext _context;
        public TicketsServices(AppDbContext context)
        {
            _context = context;
        }


        public List<Ticket> GetAll()
        {
            var result = _context.Tickets.ToList();

            return result;
        }

        public TicketDto GetById(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == id);

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
            var user = _context.Users.FirstOrDefault(x => x.Id == dto.CustomerId);
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

            _context.Tickets.Add(ticket);

            _context.SaveChanges();

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

        public TicketDto Update(int id, UpdateTicketDto dto)
        {
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == id);
            if (ticket == null)
            {
                throw new NotFoundException("Id do chamado não encontrado.");
            }

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.Status = dto.Status;
            ticket.Priority = dto.Priority;

            _context.SaveChanges();

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
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == id);
            if (ticket == null)
            {
                throw new NotFoundException("Id do chamado não encontrado.");
            }

            _context.Tickets.Remove(ticket);

            _context.SaveChanges();

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