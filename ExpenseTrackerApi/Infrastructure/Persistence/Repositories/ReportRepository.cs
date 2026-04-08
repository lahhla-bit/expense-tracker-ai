using Dapper;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Shared;
using ExpenseTrackerApi.Domain.Interfaces;

namespace ExpenseTrackerApi.Infrastructure.Persistence.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetMonthlyDashboardAsync(CancellationToken ct = default)
    {
        const string sql = """
            WITH MonthlyExpenses AS (
                SELECT 
                    c."Name" AS CategoryName,
                    SUM(e."Amount") AS TotalAmount,
                    c."HexColor" AS HexColor
                FROM "Expenses" e
                JOIN "Categories" c ON e."CategoryId" = c."Id"
                WHERE e."CreatedAt" >= DATE_TRUNC('month', CURRENT_DATE)
                GROUP BY c."Name", c."HexColor"
            )
            SELECT 
                CategoryName,
                TotalAmount,
                HexColor,
                CAST(TotalAmount * 100.0 / SUM(TotalAmount) OVER() AS FLOAT) AS Percentage
            FROM MonthlyExpenses
            ORDER BY TotalAmount DESC;
            """;

        var conn = _context.Database.GetDbConnection();

        var command = new CommandDefinition(
            sql, 
            cancellationToken: ct
        );

        var reportData = (await conn.QueryAsync<CategorySummaryDto>(command)).ToList();

        return new DashboardDto
        {
            Categories = reportData,
            TotalMonthlyAmount = reportData.Sum(x => x.TotalAmount),
            MostExpensiveCategory = reportData.FirstOrDefault()?.CategoryName ?? "Nenhuma"
        };
    }
}