using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;

    public ExpenseRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}