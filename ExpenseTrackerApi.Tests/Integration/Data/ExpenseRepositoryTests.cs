using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApi.Infrastructure.Persistence;
using ExpenseTrackerApi.Infrastructure.Persistence.Repositories;
using ExpenseTrackerApi.Domain.Entities;
using FluentAssertions;

namespace ExpenseTrackerApi.Tests.Integration.Data;

public class ExpenseRepositoryTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_DeveAdicionarDespesaNoBancoDeDados()
    {
        using var context = GetInMemoryDbContext();
        var repository = new ExpenseRepository(context);
        
        var novaDespesa = new Expense 
        { 
            Amount = 50.5m, 
            Description = "Compra de Teste", 
            CategoryId = 1,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(novaDespesa);
        await repository.SaveChangesAsync();

        var despesaSalva = await context.Expenses.FirstOrDefaultAsync();
        
        despesaSalva.Should().NotBeNull();
        despesaSalva!.Amount.Should().Be(50.5m);
        despesaSalva.Description.Should().Be("Compra de Teste");
    }
}