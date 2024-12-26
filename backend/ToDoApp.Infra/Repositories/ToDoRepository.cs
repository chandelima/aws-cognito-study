using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infra.DataContext;

namespace ToDoApp.Infra.Repositories;
internal class ToDoRepository : IToDoRepository
{
    private readonly ToDoAppDbContext _dbContext;

    public ToDoRepository(ToDoAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(ToDo entity)
    {
        await _dbContext.ToDos.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ToDo>> GetAll()
    {
        return await _dbContext.ToDos
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync();
    }

    public async Task<ToDo?> GetById(Ulid id)
    {
        return await _dbContext.ToDos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(ToDo entity)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Delete(Ulid id)
    {
        var deletedEntities = await _dbContext.ToDos
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return deletedEntities > 0;
    }

    public async Task<int> CountPending()
    {
        return await _dbContext.ToDos
            .Where(x => x.CompletionDate == null)
            .CountAsync();
    }

    public async Task<bool> Exists(Ulid id)
    {
        return await _dbContext.ToDos
            .AnyAsync(x => x.Id == id);
    }
}
