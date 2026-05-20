public class UpdateTaskResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public Status Status { get; set; }
}