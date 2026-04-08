using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ExpenseTracker.Shared;
using ExpenseTrackerApi.Domain.Interfaces;

namespace ExpenseTrackerApi.Tests.Integration.Endpoints;

public class ReportsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ReportsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetDashboard_DeveRetornarSucessoEDadosDoDashboard()
    {
        var mockReportRepo = new Mock<IReportRepository>();
        
        var mockDashboardData = new DashboardDto
        {
            TotalMonthlyAmount = 150.50m,
            Categories = new List<CategorySummaryDto> 
            { 
                new CategorySummaryDto { CategoryName = "Alimentação", TotalAmount = 150.50m } 
            }
        };

        mockReportRepo
            .Setup(repo => repo.GetMonthlyDashboardAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockDashboardData);

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                //usando o mock do repositório de relatórios para garantir um comportamento previsível durante o teste
                services.AddScoped<IReportRepository>(_ => mockReportRepo.Object);
            });
        }).CreateClient();

        var response = await client.GetAsync("/api/dashboard");

        response.EnsureSuccessStatusCode(); 
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dashboardData = await response.Content.ReadFromJsonAsync<DashboardDto>();
        
        dashboardData.Should().NotBeNull();
        dashboardData!.TotalMonthlyAmount.Should().Be(150.50m);
    }
}