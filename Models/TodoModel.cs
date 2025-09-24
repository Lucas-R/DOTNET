namespace API.Models;

public class TodoModel(string title)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; private set; } = title;

    public void Update(TodoModel todo)
    {
        Title = todo.Title;
    }
}