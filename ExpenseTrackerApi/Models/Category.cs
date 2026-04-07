using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models;

/// <summary>
/// Represents a classification for expenses (e.g., Food, Transport, Leisure).
/// </summary>
public class Category
{
    /// <summary>Unique identifier for the category.</summary>
    [Key]
    public int Id { get; set; }

    /// <summary>The display name of the category.</summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>Hexadecimal color code for UI representation (e.g., #FF5733).</summary>
    public string HexColor { get; set; } = "#FFFFFF";

    /// <summary>The icon identifier to be used in the frontend application.</summary>
    public string Icon { get; set; } = "default_icon";

    /// <summary>Collection of expenses associated with this specific category.</summary>
    public List<Expense> Expenses { get; set; } = new();
}