using QuestionService_S3_Individueel.Models;
using QuestionService_S3_Individueel.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace QuestionService_S3_Individueel.Services;

public class QuestionService : IQuestionService
{
    private readonly IMongoCollection<Question> _questionsCollection;

    public QuestionService(
        IOptions<QuestionsDatabaseSettings> questionsDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            questionsDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            questionsDatabaseSettings.Value.DatabaseName);

        _questionsCollection = mongoDatabase.GetCollection<Question>(
            questionsDatabaseSettings.Value.QuestionsCollectionName);
    }

    public async Task<List<Question>> GetQuestionAsync() =>
        await _questionsCollection.Find(_ => true).ToListAsync();

    public async Task<Question?> GetQuestionAsync(string id) =>
        await _questionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateQuestionAsync(Question newQuestion) =>
        await _questionsCollection.InsertOneAsync(newQuestion);

    public async Task UpdateQuestionAsync(string id, Question updateQuestion) =>
        await _questionsCollection.ReplaceOneAsync(x => x.Id == id, updateQuestion);

    public async Task DeleteQuestionAsync(string id) =>
        await _questionsCollection.DeleteOneAsync(x => x.Id == id);
}

