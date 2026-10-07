using helpDesk.Data;
using helpDesk.Models;

namespace helpDesk.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Ticket> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public Ticket? GetById(int id)
        {
            return _context.Tickets
                .FirstOrDefault(x => x.Id == id);
        }

        public Ticket Create(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return ticket;
        }

        public Ticket? Update(int id, Ticket ticketUpdate)
        {
            var ticket = _context.Tickets
                .FirstOrDefault(x => x.Id == id);

            if (ticket == null)
                return null;

            ticket.Title = ticketUpdate.Title;
            ticket.Description = ticketUpdate.Description;
            ticket.Status = ticketUpdate.Status;
            ticket.Priority = ticketUpdate.Priority;

            _context.SaveChanges();

            return ticket;
        }

        public Ticket? Delete(int id)
        {
            var ticket = _context.Tickets
                .FirstOrDefault(x => x.Id == id);

            if (ticket == null)
                return null;

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return ticket;
        }
    }
}
