using FluentValidation;

namespace CartIQ.Application.Files.Commands.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator(FileSettings settings)
    {
        RuleFor(command => command.SizeInBytes)
            .InclusiveBetween(1, settings.MaxSizeInBytes)
            .WithMessage($"The file must be between 1 byte and {settings.MaxSizeInBytes} bytes.");

        RuleFor(command => command.ContentType)
            .Must(extension => settings.SupportedContentTypes.Contains(
                extension, StringComparer.OrdinalIgnoreCase))
            .WithMessage("The file type isn't supported for OCR.");
    }
}