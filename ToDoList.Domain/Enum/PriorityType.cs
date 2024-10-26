using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Enum;

public enum PriorityType
{
    [Display(Name = "Низкий")]
    Low = 0,
    [Display(Name = "Средний")]
    Medium = 1,
    [Display(Name = "Высокий")]
    High = 2,
    [Display(Name = "Альфа-приоритет")]
    Alpha = 3
}