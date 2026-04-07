using Dapper;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Data;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetMonthlyDashboardAsync()
    {
        var sql = @"
            WITH MonthlyExpenses AS (
                SELECT 
                    c.""Name"" as CategoryName,
                    SUM(e.""Amount"") as TotalAmount,
                    c.""HexColor"" as HexColor
                FROM ""Expenses"" e
                JOIN ""Categories"" c ON e.""CategoryId"" = c.""Id""
                WHERE e.""CreatedAt"" >= DATE_TRUNC('month', CURRENT_DATE)
                GROUP BY c.""Name"", c.""HexColor""
            )
            SELECT 
                CategoryName,
                TotalAmount,
                CAST(TotalAmount * 100.0 / SUM(TotalAmount) OVER() AS FLOAT) as Percentage,
                HexColor
            FROM MonthlyExpenses
            ORDER BY TotalAmount DESC;";

        var conn = _context.Database.GetDbConnection();
        var reportData = await conn.QueryAsync<CategorySummaryDto>(sql);

        return new DashboardDto
        {
            Categories = reportData.ToList(),
            TotalMonthlyAmount = reportData.Sum(x => x.TotalAmount),
            MostExpensiveCategory = reportData.FirstOrDefault()?.CategoryName ?? "None"
        };
    }
}