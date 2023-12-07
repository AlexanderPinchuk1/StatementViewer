using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace StatementViewer.Models
{
    /// <summary>
    /// Модель с общей информацией ведомости и классами счетов 
    /// </summary>
    public class Statement
    {
        public Guid Id { get; set; }

        public DateTime? StartOfPeriod { get; set; }

        public DateTime? EndOfPeriod { get; set; }

        public DateTime? GenerationDate { get; set; }

        public string? BankName { get; set; }

        public string? Сurrency { get; set; }

        public string? FileName { get; set; }

        public List<AccountUnit> AccountUnits { get; set; } = [];
    }


    /// <summary>
    /// Конфигурация модели ведомости для БД
    /// </summary>
    public class DocumentConfig : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> builder)
        {
            builder.HasKey(docudent => docudent.Id);
            builder.Property(docudent => docudent.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(docudent => docudent.BankName).HasMaxLength(100);
            builder.Property(docudent => docudent.Сurrency).HasMaxLength(50);
            builder.Property(docudent => docudent.FileName).IsRequired().HasMaxLength(100);
            builder.HasMany(docudent => docudent.AccountUnits).WithOne(accountClass => accountClass.Statement);
            builder.ToTable("Statement");
        }
    }
}
