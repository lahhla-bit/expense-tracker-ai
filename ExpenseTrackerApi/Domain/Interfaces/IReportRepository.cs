using ExpenseTracker.Shared;

namespace ExpenseTrackerApi.Domain.Interfaces;

public interface IReportRepository
{
    Task<DashboardDto> GetMonthlyDashboardAsync(CancellationToken ct = default);
}