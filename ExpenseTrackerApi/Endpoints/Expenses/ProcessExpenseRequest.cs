namespace ExpenseTrackerApi.Endpoints.Expenses;

public class ProcessExpenseRequest
{
    /// <summary>
    /// Text describing the expense, e.g. "Lunch at Subway for $12". The AI will analyze this text to extract the amount, category, and description of the expense.
    /// </summary>
    public string Text { get; set; } = string.Empty;
}