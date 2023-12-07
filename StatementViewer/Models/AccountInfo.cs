using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StatementViewer.Models
{
    /// <summary>
    /// Модель для хранениия данных о счете
    /// </summary>
    public class AccountInfo
    {
        public Guid? Id { get; set; }

        public string? Number { get; set; }

        public decimal Asset { get; set; }

        public decimal Passive { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public Guid? AccountUnitId { get; set; }

        public AccountUnit? AccountUnit { get; set; }
    }


    /// <summary>
    /// Конфигурация модели информации о счете для БД
    /// </summary>
    public class AccountConfig : IEntityTypeConfiguration<AccountInfo>
    {
        public void Configure(EntityTypeBuilder<AccountInfo> builder)
        {
            builder.HasKey(account => account.Id);
            builder.Property(account => account.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(account => account.Number).IsRequired().HasMaxLength(10);
            builder.Property(account => account.Asset).HasPrecision(30, 5);
            builder.Property(account => account.Passive).HasPrecision(30, 5);
            builder.Property(account => account.Debit).HasPrecision(30, 5);
            builder.Property(account => account.Credit).HasPrecision(30, 5);
            builder.HasOne(account => account.AccountUnit).WithMany(accountClass => accountClass.AccountsInfo);
            builder.ToTable("Account");
        }
    }
}
