namespace ExpenseTrackerApi.Models;

/// <summary>
/// Represents the request sent to the AI service for processing an expense description.
/// </summary> 
/// <param name="Text">The text description of the expense that the AI will analyze to determine the amount, category, and details of the expense.</param>   
public record ProcessRequest(string Text);