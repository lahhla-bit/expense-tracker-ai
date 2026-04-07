using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Models;

/// <summary>
/// Represents a financial transaction recorded in the system.
/// </summary>
public class Expense
{
    /// <summary>Unique identifier for the expense.</summary>
    [Key]
    public int Id { get; set; }

    /// <summary>The monetary value of the expense.</summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    /// <summary>The date and time when the expense was created (UTC).</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>A brief description of what was purchased.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Indicates if the expense was categorized by AI.</summary>
    public bool IsProcessedByAI { get; set; } = false;

    // FK
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}