using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public interface IExpenseRepository
{
    Task AddAsync(Expense expense, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}