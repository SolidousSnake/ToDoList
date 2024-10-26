using ToDoList.Domain.Enum;

namespace ToDoList.Domain.Entity;

public class TaskEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public DateTime CreationDate { get; set; }
    public PriorityType Priority { get; set; }
}