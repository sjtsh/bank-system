using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking_B.Models.Configuration
{
    public class BankConfiguration : IEntityTypeConfiguration<BankModel>
    {
        void IEntityTypeConfiguration<BankModel>.Configure(EntityTypeBuilder<BankModel> builder)
        {
            builder.ToTable("Bank");

            builder.HasIndex(user => user.Name).IsUnique();

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .HasMaxLength(40)
                .IsRequired(true);

            builder.Property(i => i.TotalBalance)
                .HasDefaultValue(0);

            builder.Property(i => i.TotalWithdrawl)
                .HasDefaultValue(0);

            builder.Property(i => i.TotalDeposit)
                .HasDefaultValue(0);
        }
    }
}
