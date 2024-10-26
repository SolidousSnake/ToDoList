using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Dto.Task;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Response;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class TaskService : ITaskService
{
    private readonly IBaseRepository<TaskEntity> _repository;
    private ILogger<TaskService> _logger;

    public TaskService(IBaseRepository<TaskEntity> repository, ILogger<TaskService> logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.LogInformation($"created");
    }

    public async Task<BaseResponse<TaskEntity>> Create(CreateTaskDto dto)
    {
        try
        {
            _logger.LogInformation($"Request for creating a task - {dto.Name}");
            var task = await _repository.GetAll().Where(x => x.CreationDate.Date == DateTime.Today)
                .FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (task != null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task already exist",
                    StatusCode = StatusCode.TaskAlreadyExist
                };
            }

            task = new TaskEntity()
            {
                Name = dto.Name,
                Description = dto.Description,
                Priority = dto.Priority,
                Completed = false,
                CreationDate = DateTime.Now
            };

            await _repository.Create(task);
            _logger.LogInformation($"Task created: {task.Name} {task.CreationDate}");
            return new BaseResponse<TaskEntity>()
            {
                Description = "Task has created",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"[{nameof(Create)}]: {e.Message}");
            return new BaseResponse<TaskEntity>()
            {
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}