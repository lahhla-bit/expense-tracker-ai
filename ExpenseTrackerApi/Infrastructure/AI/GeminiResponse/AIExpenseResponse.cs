namespace ExpenseTrackerApi.Infrastructure.AI.GeminiResponse;


/// <summary>
/// Represents the response from the AI service when analyzing an expense description.
/// </summary>
public class AIExpenseResponse
{
    /// <summary>
    /// The amount of the expense as determined by the AI. This should be a positive decimal value representing the cost of the expense.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The category of the expense as determined by the AI. This should be a string representing the type of expense.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The description of the expense as determined by the AI. This should be a string representing the details of the expense.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}