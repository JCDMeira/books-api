using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreApi.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; } = null!;

    public decimal Price { get; set; }
    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Author { get; set; } = null!;


    public string edition { get; set; } = null!;
}