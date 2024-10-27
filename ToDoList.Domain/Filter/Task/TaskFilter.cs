﻿using ToDoList.Domain.Enum;

namespace ToDoList.Domain.Filter.Task;

public class TaskFilter
{
    public string Name { get; set; }
    public PriorityType? Priority { get; set; }
}