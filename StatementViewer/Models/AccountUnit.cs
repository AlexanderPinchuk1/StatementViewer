using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace StatementViewer.Models
{
    /// <summary>
    /// Модель для объединения счетов по классам
    /// </summary>
    public class AccountUnit
    {
        public Guid? Id { get; set; }

        public string? Description { get; set; }

        public string? Number { get; set; }

        public Guid StatementId { get; set; }

        public Statement? Statement { get; set; }

        public List<AccountInfo> AccountsInfo { get; set; } = [];
    }


    /// <summary>
    /// Конфигурация модели класса счетов для БД
    /// </summary>
    public class AccountClassConfig : IEntityTypeConfiguration<AccountUnit>
    {
        public void Configure(EntityTypeBuilder<AccountUnit> builder)
        {
            builder.HasKey(accountClass => accountClass.Id);
            builder.Property(accountClass => accountClass.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(accountClass => accountClass.Description).IsRequired().HasMaxLength(200);
            builder.Property(accountClass => accountClass.Number).IsRequired().HasMaxLength(10);
            builder.HasOne(accountClass => accountClass.Statement).WithMany(document => document.AccountUnits);
            builder.HasMany(accountClass => accountClass.AccountsInfo).WithOne(accountClass => accountClass.AccountUnit);
            builder.ToTable("AccountUnit");
        }
    }
}
