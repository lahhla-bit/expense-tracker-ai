using ExpenseTrackerApi.Infrastructure.AI.GeminiResponse;

namespace ExpenseTrackerApi.Domain.Interfaces;

public interface IGeminiService
{
    Task<AIExpenseResponse?> ProcessExpenseText(string userText);
}