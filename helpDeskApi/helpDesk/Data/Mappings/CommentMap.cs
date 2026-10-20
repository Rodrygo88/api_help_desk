using helpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace helpDesk.Data.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");

            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x=>x.Content)
                .IsRequired()
                .HasMaxLength(300);
            
            builder.Property(x=>x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            
            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Comments)
                .HasForeignKey(x=>x.UserId);
            
            builder.HasOne(x=> x.Ticket)
                .WithMany(x=> x.Comments)
                .HasForeignKey(x=>x.TicketId);
        }
    }
}