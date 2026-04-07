using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasPrecision(18, 2);
            
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Alimentação", HexColor = "#FF9800" },
            new Category { Id = 2, Name = "Compras",     HexColor = "#2196F3" },
            new Category { Id = 3, Name = "Transporte",  HexColor = "#4CAF50" },
            new Category { Id = 4, Name = "Lazer",       HexColor = "#9C27B0" },
            new Category { Id = 5, Name = "Saúde",       HexColor = "#F44336" },
            new Category { Id = 6, Name = "Educação",    HexColor = "#3F51B5" },
            new Category { Id = 7, Name = "Moradia",     HexColor = "#795548" },
            new Category { Id = 8, Name = "Outros",      HexColor = "#607D8B" }
        );
    }
}