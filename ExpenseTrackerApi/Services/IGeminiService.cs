using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Services;

public interface IGeminiService
{
    Task<AIExpenseResponse?> ProcessExpenseText(string userText);
}