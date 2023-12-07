using StatementViewer.Domain;
using StatementViewer.Models;

namespace StatementViewer.Foundation.Interfaces
{
    public interface IDatabaseService
    {
        public Task<List<StatementName>> GetStatements();

        public Task<Statement?> GetDocumentByIdOrReturnNullAsync(Guid documentId);

        public Task SaveStatement(Statement statement);
    }
}
