using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Models.Configuration
{
    public class UserTransactionConfiguration : IEntityTypeConfiguration<UserTransactionModel>
    {
        void IEntityTypeConfiguration<UserTransactionModel>.Configure(EntityTypeBuilder<UserTransactionModel> builder)
        {
            builder.ToTable("UserTransaction");

            builder.HasKey(i => i.Id);

            builder.HasOne(ut => ut.Reciever)
                .WithMany(u => u.RecievedTransactions)
                .HasForeignKey(t => t.RecieverId);

            builder.HasOne(ut => ut.Sender)
                .WithMany(u => u.SentTransactions)
                .HasForeignKey(t => t.SenderId);

            builder.Property(i => i.Remark)
                .HasMaxLength(40);

            builder.Property(i => i.Amount)
                .HasPrecision(2)
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("getdate()"); 
        }
    }
}
