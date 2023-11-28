namespace Quest.io.Models;

public class Todo
{
    public int? Id { get; set; }
    public string? Name { get; set; }

     // We dont use this for now
    // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
