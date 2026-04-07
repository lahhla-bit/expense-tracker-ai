using Microsoft.EntityFrameworkCore;
using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Services;
using FastEndpoints;
using Scalar.AspNetCore;
using Serilog;
using Microsoft.AspNetCore.RateLimiting;
using DotNetEnv;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() 
    .WriteTo.Console() 
    .WriteTo.File("logs/api-error-.txt", 
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
        rollingInterval: RollingInterval.Day)
    .CreateLogger();


try
{
    Env.Load();

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // FastEndpoints
    builder.Services.AddFastEndpoints();

    // Data Base
    builder.Services.AddSingleton<AuditInterceptor>();
    builder.Services.AddDbContext<AppDbContext>((sp, options) => {
        var interceptor = sp.GetRequiredService<AuditInterceptor>();
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"))
            .AddInterceptors(interceptor);
    });

    // IA
    builder.Services.AddHttpClient<IGeminiService, GeminiService>()
        .AddStandardResilienceHandler(options => {
            options.Retry.MaxRetryAttempts = 3;
            options.Retry.Delay = TimeSpan.FromSeconds(2);
            options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(10);
        });

    // Services
    builder.Services.AddScoped<IExpenseService, ExpenseService>();
    

    // Repository
    builder.Services.AddScoped<IReportRepository, ReportRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
    builder.Services.AddExceptionHandler<ExpenseTrackerApi.Middlewares.GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    // Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            document.Info.Title = "Expense Tracker AI API";
            document.Info.Version = "v1";
            document.Info.Description = "An intelligent financial API that uses Google Gemini to process expenses and PostgreSQL for data analysis.";
            return Task.CompletedTask;
        });
    });
    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict;
    });

    //Rate Limiting
    builder.Services.AddRateLimiter(options =>
    {
        options.AddFixedWindowLimiter("public-api", opt =>
        {
            opt.Window = TimeSpan.FromMinutes(5);
            opt.PermitLimit = 5;
            opt.QueueLimit = 0;
        });

        options.OnRejected = async (context, token) =>
        {
            context.HttpContext.Response.StatusCode = 429;
            await context.HttpContext.Response.WriteAsJsonAsync(new { 
                message = "Calm down! You have reached the limit of tests. Try again in 5 minutes." 
            }, token);
        };
    });

    var app = builder.Build();

    app.UseRateLimiter();
    app.UseExceptionHandler();
    
    //MIDDLEWARES
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();
    app.UseFastEndpoints();

    app.MapGet("/", () => "Expense Tracker API is Running!")
    .ExcludeFromDescription();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start.");
}
finally
{
    Log.CloseAndFlush();
}