using ToDoList.Domain.Dto.Task;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Response;

namespace ToDoList.Service.Interfaces;

public interface ITaskService
{
    public Task<BaseResponse<TaskEntity>> Create(CreateTaskDto dto);
    public Task<BaseResponse<IEnumerable<TaskDto>>> GetTasks();
}