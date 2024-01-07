using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Models.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        void IEntityTypeConfiguration<UserModel>.Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("User");

            builder.HasIndex(user => user.PhoneNumber).IsUnique();

            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasIndex(user => user.AccountNumber).IsUnique();

            builder.HasKey(i => i.Id);

            builder.HasOne(u => u.Bank)
                .WithMany(b => b.Users)
                .HasForeignKey(i => i.BankId);

            builder.Property(i => i.PhoneNumber)
                .HasMaxLength(10);

            builder.Property(i => i.FirstName)
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(i => i.MiddleName)
                .HasMaxLength(14);

            builder.Property(i => i.LastName)
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(i => i.Balance)
                .HasDefaultValue(0);

            builder.Property(i => i.IsAdmin)
                .HasDefaultValue(false);

            builder.Property(i => i.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(i => i.LastActivity)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
