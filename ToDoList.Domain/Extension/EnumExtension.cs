using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ToDoList.Domain.Extension;

public static class EnumExtension
{
    public static string GetDisplayName(this System.Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? "Undefined";
    }
}