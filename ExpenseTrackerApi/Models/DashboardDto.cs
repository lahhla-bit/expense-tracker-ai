namespace ExpenseTrackerApi.Models;

/// <summary>
/// Represents the data transfer object for the dashboard, containing the total monthly expenses, the most expensive category, and a list of category summaries for visualization purposes.
/// </summary>
public class DashboardDto
{
    /// <summary>
    /// Gets or sets the total amount of expenses for the month.
    /// </summary>
    public decimal TotalMonthlyAmount { get; set; }

    /// <summary>
    /// Gets or sets the name of the most expensive category for the month.
    /// </summary>
    public string MostExpensiveCategory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of category summaries for the month.
    /// </summary>
    public List<CategorySummaryDto> Categories { get; set; } = new();
}

/// <summary>
/// Represents a summary of expenses for a specific category, including the total amount, percentage of total expenses, and associated hex color for visualization purposes.
/// </summary>
public class CategorySummaryDto
{
    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of expenses for the category.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the percentage of the total monthly expenses for the category.
    /// </summary>
    public double Percentage { get; set; }

    /// <summary>
    /// Gets or sets the hex color for the category.
    /// </summary>
    public string HexColor { get; set; } = "#607D8B";
}