using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuestionService_S3_Individueel.Models;

public class QuestionModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Message { get; set; }
    public string CorrectAnswer { get; set; }
    public List<string> IncorrectAnswers { get; set; }
}
