using helpDesk.Models;

namespace helpDesk.Repositories
{
    public interface ITicketRepository
    {
        List<Ticket> GetAll();
        Ticket? GetById(int id);
        Ticket Create(Ticket ticket);
        Ticket? Update(int id, Ticket ticketUpdate);
        Ticket? Delete(int id);
    }
}
