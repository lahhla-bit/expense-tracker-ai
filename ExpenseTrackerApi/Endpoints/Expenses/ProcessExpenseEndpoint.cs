using FastEndpoints;
using ExpenseTrackerApi.Domain.Interfaces;

namespace ExpenseTrackerApi.Endpoints.Expenses;

public class ProcessExpenseEndpoint : Endpoint<ProcessExpenseRequest, ProcessExpenseResponse>
{
    private readonly IExpenseService _expenseService;

    public ProcessExpenseEndpoint(IExpenseService expenseService)
    {
        _expenseService = expenseService;
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

    public override async Task HandleAsync(ProcessExpenseRequest req, CancellationToken ct)
    {
        var expense = await _expenseService.ProcessAndSaveExpenseAsync(req.Text, ct);

        Response = new ProcessExpenseResponse
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Description = expense.Description,
            CategoryName = expense.Category?.Name ?? "Outros"
        };
    }
}