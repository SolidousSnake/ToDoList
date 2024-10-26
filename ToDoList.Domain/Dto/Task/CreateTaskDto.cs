using ToDoList.Domain.Enum;

namespace ToDoList.Domain.Dto.Task;

public class CreateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public PriorityType Priority { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentNullException(Name, "Write name for task");
        
        if (string.IsNullOrWhiteSpace(Description))
            throw new ArgumentNullException(Description, "Write description for task");
    }
}