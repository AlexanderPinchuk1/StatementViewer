using StatementViewer.Models;

namespace StatementViewer.Foundation.Interfaces
{
    public interface IStatementParserService
    {
        public Task<Statement> ParseAsync(string filePath);
    }
}
