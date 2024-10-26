using ToDoList.Domain.Enum;

namespace ToDoList.Domain.Dto.Task;

public class CreateTaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public PriorityType Priority { get; set; }

}