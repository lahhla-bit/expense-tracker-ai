using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public interface IReportRepository
{
    Task<DashboardDto> GetMonthlyDashboardAsync(CancellationToken ct = default);
}