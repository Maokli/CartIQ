namespace CartIQ.Application.Files;

public class FileSettings
{
    /// <summary>Maximum allowed file size in bytes.</summary>
    public long MaxSizeInBytes { get; init; }

    /// <summary>Allowed file extensions (with leading dot) for OCR processing.</summary>
    public string[] SupportedContentTypes { get; init; } = [];
}