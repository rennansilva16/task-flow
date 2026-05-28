public class UpdateTaskResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; } = null;
    public DateOnly? DueDate { get; set; }
    public Status Status { get; set; }
}