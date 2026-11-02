using CartIQ.Application.Common.Models;
using CartIQ.Application.Files.Commands.UploadFile;
using CartIQ.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CartIQ.WebApi.Endpoints;

public class Files : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(UploadFile, "Upload");
    }
    
    
    [EndpointSummary("Update receipt.")]
    [EndpointDescription("Updates a receipt with a given Id.")]
    public static async Task<Results<BadRequest, NoContent>> UploadFile(ISender sender, [FromForm]IFormFile file , CancellationToken cancellationToken)
    {
        byte[] fileContent = null;
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            fileContent = ms.ToArray();
        }
        
        var command = new UploadFileCommand(content: fileContent, 
            contentType: file.ContentType, 
            fileName: file.FileName,
            sizeInBytes: file.Length);
        var result = await sender.Send(command, cancellationToken);
        
        if(!result.ResultItem)
            return TypedResults.BadRequest();
        
        return TypedResults.NoContent();
    }
}