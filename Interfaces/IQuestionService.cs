using QuestionService_S3_Individueel.Models;
using MongoDB.Driver;

namespace QuestionService_S3_Individueel.Interfaces
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestionAsync();

        Task<Question?> GetQuestionAsync(string id);

        Task CreateQuestionAsync(Question newQuestion);

        Task UpdateQuestionAsync(string id, Question updateQuestion);

        Task DeleteQuestionAsync(string id);
    }
}
