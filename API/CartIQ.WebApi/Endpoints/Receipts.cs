using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.Commands.DeleteReceipt;
using CartIQ.Application.Receipts.Commands.UpdateReceipt;
using CartIQ.Application.Receipts.DTOs;
using CartIQ.Application.Receipts.Queries.GetReceipt;
using CartIQ.Application.Receipts.Queries.GetReceipts;
using CartIQ.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CartIQ.WebApi.Endpoints;

public class Receipts : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetReceipts);
        groupBuilder.MapGet(GetReceiptDetails, "{id}");
        groupBuilder.MapPatch(UpdateReceipt, "Update");
        groupBuilder.MapDelete(DeleteReceipt, "{id}");
    }

    [EndpointSummary("Get a list of receipts.")]
    [EndpointDescription("Gets the list of receipts of authorized user following pagination params.")]
    public static async Task<Ok<Result<PaginationResult<ReceiptDto>>>> GetReceipts(ISender sender, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetReceiptsQuery(pageIndex, pageSize);
        var receipts = await sender.Send(query, cancellationToken);
        
        return TypedResults.Ok(receipts);
    }
    
    [EndpointSummary("Get receipt details.")]
    [EndpointDescription("Gets the details for a given receipt along with its items.")]
    public static async Task<Ok<Result<ReceiptDetailsDto>>> GetReceiptDetails(ISender sender, Guid Id, CancellationToken cancellationToken)
    {
        var query = new GetReceiptDetailsQuery(Id);
        var receiptDetails = await sender.Send(query, cancellationToken);
        
        return TypedResults.Ok(receiptDetails);
    }
    
    [EndpointSummary("Delete receipt.")]
    [EndpointDescription("Deletes a receipt with a given Id.")]
    public static async Task<Ok<Result<bool>>> DeleteReceipt(ISender sender, Guid Id, CancellationToken cancellationToken)
    {
        var command = new DeleteReceiptCommand(Id);
        var result = await sender.Send(command, cancellationToken);
        
        return TypedResults.Ok(result);
    }
    
    
    [EndpointSummary("Update receipt.")]
    [EndpointDescription("Updates a receipt with a given Id.")]
    public static async Task<Ok<Result<bool>>> UpdateReceipt(ISender sender, UpdateReceiptCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return TypedResults.Ok(result);
    }
}