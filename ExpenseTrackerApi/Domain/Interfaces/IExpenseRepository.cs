using ExpenseTrackerApi.Domain.Entities;

namespace ExpenseTrackerApi.Domain.Interfaces;

public interface IExpenseRepository
{
    Task AddAsync(Expense expense, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}