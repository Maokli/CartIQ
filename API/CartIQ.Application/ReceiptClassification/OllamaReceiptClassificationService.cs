using CartIQ.Application.ReceiptClassification.DTOs;
using CartIQ.Application.ReceiptClassification.Interfaces;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace CartIQ.Application.ReceiptClassification;

public class OllamaReceiptClassificationService : IReceiptClassificationService
{
    private Func<ChatMessage> _classificationPrompt = () => new ChatMessage(
        ChatRole.User,
        """
        Extract the merchant, currency, and all product line items
        from the following OCR result.

        The OCR text may contain recognition errors.
        Use your best judgment to reconstruct product names,
        but do not invent products or prices.

        Return all identifiable line items.
        """
    );

    private readonly ILogger<OllamaReceiptClassificationService> _logger;
    private readonly IChatClient _chatClient;

    public OllamaReceiptClassificationService(ILogger<OllamaReceiptClassificationService> logger,
        IChatClient chatClient)
    {
        _logger = logger;
        _chatClient = chatClient;
    }

    public async Task<ClassificationResultDto> ClassifyAsync(string rawOcrResult, CancellationToken cancellationToken)
    {
        var message = _classificationPrompt();
        message.Contents.Add(
            new TextContent(rawOcrResult)
        );
        _logger.LogDebug("Sending prompt to llm. \n prompt: {prompt}", message);
        
        var response = await _chatClient.GetResponseAsync<ClassificationResultDto>([message],
            new ChatOptions
            {
                Temperature = 0
            },
            true,
            cancellationToken
        );
        _logger.LogDebug("Received response from LLM: {response}", response);

        return response.Result;
    }
}