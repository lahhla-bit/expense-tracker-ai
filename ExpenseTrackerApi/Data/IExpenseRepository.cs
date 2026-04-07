using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public interface IExpenseRepository
{
    Task AddAsync(Expense expense);
    Task SaveChangesAsync();
}