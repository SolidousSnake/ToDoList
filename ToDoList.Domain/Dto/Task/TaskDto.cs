using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Dto.Task;

public class TaskDto
{
    public long Id { get; set; }
    [Display(Name = "Название")]
    public string Name { get; set; }
    [Display(Name = "Описание")]
    public string Description { get; set; }
    [Display(Name = "Готовность")]
    public string Completed { get; set; }
    [Display(Name = "Дата создания")]
    public string CreationDate { get; set; }
    [Display(Name = "Приоритет")]
    public string Priority { get; set; }
}