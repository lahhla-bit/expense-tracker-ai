using FastEndpoints;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Endpoints.Reports;

public class GetDashboardEndpoint : EndpointWithoutRequest<DashboardDto>
{
    private readonly IReportRepository _reportRepo;

    public GetDashboardEndpoint(IReportRepository reportRepo)
    {
        _reportRepo = reportRepo;
    }

    public override void Configure()
    {
        Get("/api/dashboard");
        AllowAnonymous();
        Description(d => d
            .WithSummary("Get monthly financial dashboard")
            .WithDescription("Returns totals grouped by category with percentage analysis."));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _reportRepo.GetMonthlyDashboardAsync(ct);
        Response = result;
    }
}