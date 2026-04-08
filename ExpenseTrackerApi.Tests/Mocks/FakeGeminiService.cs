using ExpenseTrackerApi.Domain.Interfaces;
using ExpenseTrackerApi.Infrastructure.AI.GeminiResponse;

namespace ExpenseTrackerApi.Tests.Mocks;

/// <summary>
/// Uma implementação falsa do serviço Gemini para ser usada em testes de integração.
/// Evita chamadas de rede reais e garante um comportamento previsível.
/// </summary>
public class FakeGeminiService : IGeminiService
{
    public Task<AIExpenseResponse?> ProcessExpenseText(string text)
    {
        var response = new AIExpenseResponse
        {
            Amount = 15.50m,
            Description = "Despesa gerada por Teste Automatizado",
            Category = "Alimentação"
        };

        return Task.FromResult<AIExpenseResponse?>(response);
    }
}