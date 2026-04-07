using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;

    public ExpenseRepository(AppDbContext context) => _context = context;

    /// <summary>
    /// Adds a new expense to the database context. This method does not save changes to the database; it only adds the expense to the context. To persist changes, call SaveChangesAsync after adding all desired expenses.    
    /// </summary>
    /// <param name="expense">The expense to add.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddAsync(Expense expense, CancellationToken ct = default)
    {
        await _context.Expenses.AddAsync(expense, ct);
    }

    /// <summary>
    /// Saves all changes made in the context to the database. This method should be called after adding, updating, or removing entities to persist those changes to the database. It is important to call this method to ensure that all operations performed on the context are reflected in the database.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}