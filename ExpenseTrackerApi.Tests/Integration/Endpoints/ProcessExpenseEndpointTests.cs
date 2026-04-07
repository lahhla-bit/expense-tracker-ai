using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ExpenseTrackerApi.Endpoints.Expenses;

namespace ExpenseTrackerApi.Tests.Integration.Endpoints;

public class ProcessExpenseEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProcessExpenseEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProcessExpense_DeveRetornarBadRequest_QuandoTextoInvalido()
    {
        var request = new ProcessExpenseRequest { Text = "Oi" };
        var response = await _client.PostAsJsonAsync("/api/expenses/process", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}