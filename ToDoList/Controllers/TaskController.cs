using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Dto.Task;
using ToDoList.Service.Interfaces;

namespace ToDoList.Controllers;

public class TaskController : Controller
{
    private readonly ITaskService _taskService;
    
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var response = await _taskService.Create(dto);

        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            return Ok(new { description = response.Description });

        return BadRequest(new { description = response.Description });
    }
}