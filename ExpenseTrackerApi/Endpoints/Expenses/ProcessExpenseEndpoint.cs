using FastEndpoints;
using ExpenseTrackerApi.Models;
using ExpenseTrackerApi.Services;
using ExpenseTrackerApi.Data;

namespace ExpenseTrackerApi.Endpoints.Expenses;

public class ProcessExpenseEndpoint : Endpoint<ProcessRequest, object>
{
    private readonly GeminiService _gemini;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IExpenseRepository _expenseRepo;
    private readonly ILogger<ProcessExpenseEndpoint> _logger;

    public ProcessExpenseEndpoint(
        GeminiService gemini, 
        ICategoryRepository categoryRepo, 
        IExpenseRepository expenseRepo,
        ILogger<ProcessExpenseEndpoint> logger)
    {
        _gemini = gemini;
        _categoryRepo = categoryRepo;
        _expenseRepo = expenseRepo;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/api/expenses/process");
        AllowAnonymous();
        Options(x => x.RequireRateLimiting("public-api"));
        Description(d => d
            .WithSummary("Process expense text via AI")
            .WithDescription("Uses Gemini to categorize and save expenses. Input should be raw text."));
    }

    public override async Task HandleAsync(ProcessRequest req, CancellationToken ct)
    {
        AIExpenseResponse? aiResult;

        try
        {
            aiResult = await _gemini.ProcessExpenseText(req.Text);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Critical Error: Failed to communicate with the Gemini API.");
            
            ThrowError("The AI service is temporarily unavailable.");
            return;
        }

        if (aiResult == null)
        {
            ThrowError("AI processing failed. Could not interpret the text.");
            return;
        }

        var category = await _categoryRepo.GetByNameAsync(aiResult.Category) 
                       ?? await _categoryRepo.GetOrCreateOthersAsync();

        var expense = new Expense
        {
            Amount = aiResult.Amount,
            Description = aiResult.Description,
            CategoryId = category.Id,
            IsProcessedByAI = true
        };

        await _expenseRepo.AddAsync(expense);
        await _expenseRepo.SaveChangesAsync();

        Response = new {
            expense.Id,
            Category = category.Name,
            expense.Amount,
            expense.Description
        };
    }
}