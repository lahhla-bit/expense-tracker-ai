# Expense Tracker AI API

An intelligent financial management ecosystem built with **.NET 10**, designed to transform raw text into structured financial data using **Google Gemini AI**. This project features a robust **Minimal API**, a modern **Blazor WebAssembly** dashboard, and a comprehensive **Automated Testing** suite.

## Key Features

* **AI-Powered Categorization**: Processes natural language (e.g., "Spent $45 on pizza") using Gemini 1.5 Flash via the API.
* **Modern Dashboard (Blazor)**: A professional dark-themed frontend built with **MudBlazor**, featuring interactive charts and real-time data visualization.
* **Shared Architecture**: Uses a **Shared Class Library** to ensure 100% type safety and code reuse between Backend and Frontend.
* **Automated Testing Suite**: 
    * **Unit Tests**: Focused on business logic and service validation.
    * **Integration Tests**: Validates endpoints and database repositories using `WebApplicationFactory` and `InMemoryDatabase`.
* **CI/CD Pipeline**: Integrated with **GitHub Actions** to automatically build and run the entire test suite on every `push` or `pull_request`.
* **Resilience & Stability**: Implements standard resilience patterns (Polly) with automatic retries for AI service calls.
* **Security**: Built-in protection with Fixed Window Rate Limiting and strict Input Validation (FluentValidation).
* **Modern Documentation**: Interactive API testing powered by **Scalar**.

## Tech Stack

* **Backend**: .NET 10 (Minimal APIs), Entity Framework Core, Dapper.
* **Frontend**: Blazor WebAssembly, MudBlazor (Material Design).
* **Database**: PostgreSQL 15 (Dockerized) & EF Core InMemory (for testing).
* **AI Engine**: Google Gemini AI.
* **Validation**: FluentValidation
* **Logging**: Serilog
* **Resilience**: Microsoft.Extensions.Http.Resilience (Polly-based)
* **Testing**: xUnit, Moq, FluentAssertions, Microsoft.AspNetCore.Mvc.Testing.
* **DevOps**: GitHub Actions (CI), Docker & Docker Compose.
* **Architecture**: Mono-repo with Shared DTOs.

## Project Structure

* `ExpenseTrackerApi/`: The core REST API.
* `ExpenseTracker.Client/`: The Blazor WebAssembly frontend.
* `ExpenseTracker.Shared/`: Shared models and DTOs used by both projects.
* `ExpenseTrackerApi.Tests/`: Unit and Integration tests.

## How to Run

### 1. Prerequisites
* Docker installed.
* .NET 10 SDK installed.
* Google Gemini API Key ([Get it here](https://aistudio.google.com/)).

### 2. Database Setup (Docker)
In the root folder, spin up the PostgreSQL container:
```bash
docker compose up -d
```

### 3. Configuration
Create a .env file in ExpenseTrackerApi/ based on .env.example:
```json
Gemini__ApiKey=YOUR_KEY
ConnectionStrings__PostgreSqlConnection=Host=localhost;Port=5433;Database=expense_db;Username=postgres;Password=your_pass
```

### 4. Running the Application
To see the full ecosystem working, you need to run both the API and the Client:
**Start the API:**
```bash
cd ExpenseTrackerApi
dotnet run
```

**Start the Dashboard (New Terminal):**
```bash
cd ExpenseTracker.Client
dotnet watch
```
Access the dashboard at: http://localhost:5085

### 5. Running Tests
To execute the automated test suite and check code quality:
```bash
dotnet test
```

## API Endpoints
**Documentation**
**GET /scalar/v1:** Access the interactive testing playground.

**Expenses**
**POST /api/expenses/process:** Send a raw text string to be categorized and saved.
Constraints: Min 5, Max 250 characters.
Rate Limit: 5 requests per minute.
Input: { "text": "Almoço na Disney por $10.00" }

**Reports**
**GET /api/dashboard:** Returns monthly totals, category breakdown (with Hex colors), and spending percentages.

Developed for portfolio purposes focusing on Full-Stack .NET 10 Architecture, AI Integration, and DevOps best practices.