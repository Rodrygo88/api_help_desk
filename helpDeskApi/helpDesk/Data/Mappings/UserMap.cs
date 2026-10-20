using helpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace helpDesk.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(x=>x.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasIndex(x=>x.Email)
                .IsUnique();
            
            builder.Property(x=> x.PasswordHash)
                .IsRequired();
            
            builder.Property(x=> x.Role)
                .IsRequired();
            
            builder.Property(x=>x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}