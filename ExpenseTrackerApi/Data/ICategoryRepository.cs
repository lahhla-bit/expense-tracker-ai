using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name);
    Task<Category> GetOrCreateOthersAsync();
}