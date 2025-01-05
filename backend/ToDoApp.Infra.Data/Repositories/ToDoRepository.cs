using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infra.Data.DataContext;

namespace ToDoApp.Infra.Data.Repositories;
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

    public async Task<List<ToDo>> GetAllByUserId(string userId)
    {
        return await _dbContext.ToDos
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync();
    }

    public async Task<ToDo?> GetById(Ulid id, string userId)
    {
        return await _dbContext.ToDos
            .FirstOrDefaultAsync(x => x.Id == id  && x.UserId == userId);
    }

    public async Task Update(ToDo entity)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Delete(Ulid id, string userId)
    {
        var deletedEntities = await _dbContext.ToDos
            .Where(x => x.Id == id && x.UserId == userId)
            .ExecuteDeleteAsync();

        return deletedEntities > 0;
    }

    public async Task<int> CountPending(string userId)
    {
        return await _dbContext.ToDos
            .Where(x => x.CompletionDate == null && x.UserId == userId)
            .CountAsync();
    }

    public async Task<bool> Exists(Ulid id, string userId)
    {
        return await _dbContext.ToDos
            .AnyAsync(x => x.Id == id && x.UserId == userId);
    }
}
