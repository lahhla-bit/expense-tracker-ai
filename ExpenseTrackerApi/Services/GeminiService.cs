using System.Text.Json;
using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Services;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Gemini:ApiKey"] ?? throw new Exception("Gemini API Key not found!");
    }

    public async Task<AIExpenseResponse?> ProcessExpenseText(string userText)
    {
        var prompt = $@"
            Analise o seguinte texto de despesa: '{userText}'.
            Extraia o valor financeiro, a categoria e crie uma descrição limpa.
            
            Você DEVE classificar a despesa escolhendo EXATAMENTE UMA das seguintes categorias:
            - Alimentação
            - Compras
            - Transporte
            - Lazer
            - Saúde
            - Educação
            - Moradia
            
            Se a despesa não se encaixar em nenhuma destas opções, use EXATAMENTE a palavra 'Outros'.
            
            Retorne APENAS um objeto JSON válido, sem formatação markdown, com estas chaves exatas (em inglês para o sistema ler): 
            'amount' (decimal, apenas números, use ponto para decimais), 
            'category' (string, usando uma das opções da lista acima), 
            'description' (string, formatada de forma profissional em português).
            
            Exemplo: {{ ""amount"": 9.90, ""category"": ""Alimentação"", ""description"": ""Almoço na Disney"" }}";

        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var url = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={_apiKey}";

        var response = await _httpClient.PostAsJsonAsync(url, requestBody);
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(jsonResponse);


        using var doc = JsonDocument.Parse(jsonResponse);
        var aiText = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text").GetString();

        var cleanJson = aiText?.Replace("```json", "").Replace("```", "").Trim();

        return JsonSerializer.Deserialize<AIExpenseResponse>(cleanJson!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}