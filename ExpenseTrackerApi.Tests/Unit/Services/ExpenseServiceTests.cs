using System.ComponentModel.DataAnnotations;
using Moq;
using FluentAssertions;
using ExpenseTrackerApi.Domain.Entities;
using ExpenseTrackerApi.Domain.Interfaces;
using ExpenseTrackerApi.Application.Service;
using ExpenseTrackerApi.Infrastructure.AI.GeminiResponse;

namespace ExpenseTrackerApi.Tests.Unit.Services;

public class ExpenseServiceTests
{
    private readonly Mock<IGeminiService> _geminiMock;
    private readonly Mock<IExpenseRepository> _expenseRepoMock;
    private readonly Mock<ICategoryRepository> _categoryRepoMock;
    private readonly ExpenseService _expenseService;

    public ExpenseServiceTests()
    {
        _geminiMock = new Mock<IGeminiService>();
        _expenseRepoMock = new Mock<IExpenseRepository>();
        _categoryRepoMock = new Mock<ICategoryRepository>();

        _expenseService = new ExpenseService(
            _geminiMock.Object, 
            _expenseRepoMock.Object, 
            _categoryRepoMock.Object);
    }

    [Fact]
    public async Task ProcessAndSaveExpense_DeveSalvarDespesa_QuandoAIAceitar()
    {
        var rawText = "Comprei um café por 5 euros";
        var aiResponse = new AIExpenseResponse { Amount = 5.0m, Description = "Café", Category = "Alimentação" };
        var category = new Category { Id = 1, Name = "Alimentação" };

        _geminiMock.Setup(x => x.ProcessExpenseText(rawText)).ReturnsAsync(aiResponse);
        _categoryRepoMock.Setup(x => x.GetByNameAsync("Alimentação")).ReturnsAsync(category);

        var result = await _expenseService.ProcessAndSaveExpenseAsync(rawText, CancellationToken.None);

        result.Should().NotBeNull();
        result.Amount.Should().Be(5.0m);
        result.CategoryId.Should().Be(1);

        _expenseRepoMock.Verify(x => x.AddAsync(It.IsAny<Expense>(), It.IsAny<CancellationToken>()), Times.Once);
        _expenseRepoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ProcessAndSaveExpense_DeveLancarExcecao_QuandoValorInvalido()
    {

        var rawText = "Comprei algo grátis";
        var aiResponse = new AIExpenseResponse { Amount = 0, Description = "Grátis", Category = "Outros" };

        _geminiMock.Setup(x => x.ProcessExpenseText(rawText)).ReturnsAsync(aiResponse);

        Func<Task> action = async () => await _expenseService.ProcessAndSaveExpenseAsync(rawText, CancellationToken.None);

        await action.Should().ThrowAsync<ValidationException>()
            .WithMessage("The AI response is invalid. Amount must be greater than zero.");
    }
}