using FastEndpoints;
using FluentValidation;
using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Endpoints.Expenses;

public class ProcessExpenseValidator : Validator<ProcessRequest>
{
    public ProcessExpenseValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Expense text cannot be empty.")
            
            .MinimumLength(5)
            .WithMessage("Text is too short for AI processing. (Minimum 5 characters)")
            
            .MaximumLength(150)
            .WithMessage("Text is too long. Please provide a concise expense description.");
    }
}