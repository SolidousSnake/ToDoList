using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Dto.Task;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Extension;
using ToDoList.Domain.Filter.Task;
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
            dto.Validate();
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
                Description = $"{e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<bool>> EndTask(long id)
    {
        try
        {
            var task = await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.TaskNotFound,
                    Description = "Task not found"
                };
            }

            task.Completed = true;
            await _repository.Update(task);
            
            return new BaseResponse<bool>()
            {
                Description = $"Task completed",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"[{nameof(EndTask)}]: {e.Message}");
            return new BaseResponse<bool>()
            {
                Description = $"{e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<TaskDto>>> GetTasks(TaskFilter filter)
    {
        try
        {
            var task = await _repository.GetAll()
                .Where(x => !x.Completed)
                .WhereIf(!string.IsNullOrWhiteSpace(filter.Name), x => x.Name == filter.Name)
                .WhereIf(filter.Priority.HasValue, x => x.Priority == filter.Priority)
                .Select(x => new TaskDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Completed = x.Completed ? "Готова" : "В процессе",
                Priority = x.Priority.GetDisplayName(),
                CreationDate = x.CreationDate.ToLongDateString()
            }).ToListAsync();

            return new BaseResponse<IEnumerable<TaskDto>>()
            {
                Data = task,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"[{nameof(GetTasks)}]: {e.Message}");
            return new BaseResponse<IEnumerable<TaskDto>>()
            {
                Description = $"{e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}