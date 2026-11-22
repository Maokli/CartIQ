namespace CartIQ.Infrastructure.OCR;

public class McpOcrApiHttpClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<string> ProcessAsync(
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();

        using var fileContent = new StreamContent(fileStream);

        content.Add(fileContent, "file", fileName);

        using var response = await _httpClient.PostAsync(
            "api/v1/ocr",
            content,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}