using ExpenseTrackerApi.Domain.Entities;

namespace ExpenseTrackerApi.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name);
    Task<Category> GetOrCreateOthersAsync();
}