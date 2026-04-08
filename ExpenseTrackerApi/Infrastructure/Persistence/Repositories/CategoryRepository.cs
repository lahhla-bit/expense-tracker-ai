using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApi.Domain.Entities;
using ExpenseTrackerApi.Domain.Interfaces;

namespace ExpenseTrackerApi.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) => _context = context;

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
    }

    public async Task<Category> GetOrCreateOthersAsync()
    {
        var others = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == "Outros");

        if (others == null)
        {
            others = new Category { Name = "Outros", HexColor = "#9E9E9E" };
            _context.Categories.Add(others);
            await _context.SaveChangesAsync();
        }

        return others;
    }
}