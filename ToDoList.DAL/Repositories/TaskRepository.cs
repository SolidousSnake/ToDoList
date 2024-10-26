using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL.Repositories;

public class TaskRepository(AppDbContext dbContext) : IBaseRepository<TaskEntity>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(TaskEntity entity)
    {
        await _dbContext.Tasks.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(TaskEntity entity)
    {
        _dbContext.Tasks.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TaskEntity> Update(TaskEntity entity)
    {
        _dbContext.Tasks.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public IQueryable<TaskEntity> GetAll() => _dbContext.Tasks;
}