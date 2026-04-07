using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Services;
public class ExpenseService : IExpenseService
{
    private readonly IGeminiService _geminiService;
    //private readonly AppDbContext _context;
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ExpenseService(IGeminiService geminiService, IExpenseRepository expenseRepository, ICategoryRepository categoryRepository)
    {
        _geminiService = geminiService;
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Expense> ProcessAndSaveExpenseAsync(string rawText, CancellationToken ct)
    {
        var aiResponse = ValidateAiResponse(await _geminiService.ProcessExpenseText(rawText));

        var category = await _categoryRepository.GetByNameAsync(aiResponse.Category);

        var expense = new Expense
        {
            Amount = aiResponse.Amount,
            Description = aiResponse.Description ?? "Despesa AI",
            CategoryId = category?.Id ?? 8,
            IsProcessedByAI = true,
            CreatedAt = DateTime.UtcNow
        };

        await _expenseRepository.AddAsync(expense, ct);
        await _expenseRepository.SaveChangesAsync(ct);

        return expense;
    }

    private AIExpenseResponse ValidateAiResponse(AIExpenseResponse? response)
    {
        if (response == null || response.Amount <= 0)
            throw new ValidationException("The AI response is invalid. Amount must be greater than zero.");
            
        return response;
    }
}