namespace StatementViewer.Domain
{
    /// <summary>
    ///  Модель для вывода списка доступных к просмотру ведомостей
    /// </summary>
    public class StatementName
    {
        public Guid Id { get; set;}

        public string? Name { get; set;}
    }
}
