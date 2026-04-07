using ExpenseTrackerApi.Models;

public interface IExpenseService
{
    Task<Expense> ProcessAndSaveExpenseAsync(string rawText, CancellationToken ct);
}