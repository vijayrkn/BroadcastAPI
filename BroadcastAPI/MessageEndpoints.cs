using Microsoft.EntityFrameworkCore;
using BroadcastAPI.Data;
using BroadcastAPI.Models;
namespace BroadcastAPI;

public static class MessageEndpoints
{
    public static void MapMessageEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Message", async (BroadcastAPIContext db) =>
        {
            return await db.Message.ToListAsync();
        })
        .WithName("GetAllMessages")
        .Produces<List<Message>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Message/{id}", async (int Id, BroadcastAPIContext db) =>
        {
            return await db.Message.FindAsync(Id)
                is Message model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetMessageById")
        .Produces<Message>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Message/{id}", async (int Id, Message message, BroadcastAPIContext db) =>
        {
            var foundModel = await db.Message.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateMessage")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Message/", async (Message message, BroadcastAPIContext db) =>
        {
            db.Message.Add(message);
            await db.SaveChangesAsync();
            return Results.Created($"/Messages/{message.Id}", message);
        })
        .WithName("CreateMessage")
        .Produces<Message>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Message/{id}", async (int Id, BroadcastAPIContext db) =>
        {
            if (await db.Message.FindAsync(Id) is Message message)
            {
                db.Message.Remove(message);
                await db.SaveChangesAsync();
                return Results.Ok(message);
            }

            return Results.NotFound();
        })
        .WithName("DeleteMessage")
        .Produces<Message>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
