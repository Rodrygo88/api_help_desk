using helpDesk.Enums;
using helpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace helpDesk.Data.Mappings
{
    public class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("tickets");

            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(x=>x.Title)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(x=>x.Description)
                .IsRequired()
                .HasMaxLength(300);
            
            builder.Property(x=> x.Status)
                .IsRequired()
                .HasDefaultValue(TicketStatus.Open);
            
            builder.Property(x=>x.Priority)
                .IsRequired();
            
            builder.Property(x=>x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(x=>x.CustomerUser)
                .WithMany(x=> x.CustomerTickets)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.AssignedUser)
                .WithMany(x => x.AssignedTickets)
                .HasForeignKey(x => x.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}