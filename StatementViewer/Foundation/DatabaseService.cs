using StatementViewer.Foundation.Interfaces;
using StatementViewer.Domain;
using StatementViewer.Repositories;
using Microsoft.EntityFrameworkCore;
using StatementViewer.Models;

namespace StatementViewer.Foundation
{
    /// <summary>
    /// Сервис для работы с БД
    /// </summary>
    public class DatabaseService(StatementViewerDbContext statementViewerDbContext): IDatabaseService
    {
        private readonly StatementViewerDbContext _statementViewerDbContext = statementViewerDbContext;



        public async Task<List<StatementName>> GetStatements()
        {
            return await _statementViewerDbContext.Documents.Select(document => new StatementName()
            {
                Id = document.Id,
                Name = document.FileName
            }).ToListAsync();
        }

        public async Task<Statement?> GetDocumentByIdOrReturnNullAsync(Guid documentId)
        {
            return await _statementViewerDbContext.Documents
                .Include(document => document.AccountUnits)
                    .ThenInclude(accountClass => accountClass.AccountsInfo)
                .FirstOrDefaultAsync(document => document.Id == documentId);
        }

        public async Task SaveStatement(Statement statement)
        {
            _statementViewerDbContext.Add(statement);
            await _statementViewerDbContext.SaveChangesAsync();
        }
    }
}