# Expense Tracker AI API

An intelligent financial management API built with **.NET 10**, designed to transform raw text into structured financial data using **Google Gemini AI**.

## Key Features

* **AI-Powered Categorization**: Processes natural language (e.g., "Spent $45 on pizza") using Gemini 1.5 Flash.
* **Advanced Data Analysis**: Dashboard built with **PostgreSQL Window Functions** and **CTEs** for monthly spending insights, including category percentages and color-coded reporting.
* **Resilience & Stability (Polly):** Implements standard resilience patterns with automatic retries and timeouts for AI service calls, ensuring high availability even during provider instability.
* **Security & Rate Limiting:** Built-in protection against abuse using Fixed Window Rate Limiting (5 requests/minute per IP) to manage API costs and prevent bot attacks.
* **Input Validation:** Strict request validation using FluentValidation to ensure data integrity and prevent prompt injection or excessive token usage (text limit: 150 chars).
* **Professional Architecture**: Uses **Entity Framework Core Interceptors** for automated data auditing and **Global Exception Handling** for robust error management.
* **Structured Logging**: Integrated with **Serilog** for real-time console tracking and daily error log files.
* **Modern Documentation**: Interactive API testing and documentation powered by **Scalar** (Postman-style UI).
* **Automatic Data Seeding**: Pre-configured financial categories with professional HEX color palettes.

## Tech Stack

* **Runtime**: .NET 10 (Minimal APIs)
* **Database**: PostgreSQL 15 (Dockerized)
* **AI Engine**: Google Gemini AI (via HTTP Client & JSON Schema)
* **Resilience**: Microsoft.Extensions.Http.Resilience (Polly-based)
* **ORM**: Entity Framework Core (Migrations & Seeding) & Dapper (High-performance reporting)
* **Validation**: FluentValidation
* **Logging**: Serilog
* **DevOps**: Docker & Docker Compose
* **API UI**: Scalar

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
Update ExpenseTrackerApi/appsettings.json with your Gemini API Key and Connection String:
```json
"Gemini": {
"ApiKey": "YOUR_KEY_HERE"
},
"ConnectionStrings": {
"PostgreSqlConnection": "Host=localhost;Port=5433;Database=expense_db;Username=YOUR_USER;Password=YOUR_PASS"
}
```

#### Environment Variables
This project uses DotNetEnv to manage sensitive credentials and prevent them from being exposed in the source code.

To run this application locally, you must create a .env file in the root directory. You can use the provided .env.example as a template.

**Setup Instructions**:
Create a file named .env in the project root.

Add the following variables (replacing the placeholders with your actual data):

Gemini__ApiKey=**YOUR_ACTUAL_API_KEY_HERE**
ConnectionStrings__PostgreSqlConnection=**Host=localhost;Port=5433;Database=expense_db;Username=postgres;Password=your_password**


### 4. Run Migrations & Start
```bash
cd ExpenseTrackerApi
dotnet ef database update
dotnet run
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

Developed for portfolio purposes using Clean Code principles.