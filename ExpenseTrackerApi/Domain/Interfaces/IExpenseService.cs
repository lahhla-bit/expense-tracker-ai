using ExpenseTrackerApi.Domain.Entities;

namespace ExpenseTrackerApi.Domain.Interfaces;

public interface IExpenseService
{
    Task<Expense> ProcessAndSaveExpenseAsync(string rawText, CancellationToken ct);
}