using Microsoft.AspNetCore.Mvc;
using QuestionService_S3_Individueel.Interfaces;
using QuestionService_S3_Individueel.Models;
using QuestionService_S3_Individueel.Services;

namespace QuestionService_S3_Individueel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly QuestionService _questionsSservice;

    public QuestionsController(QuestionService questionsService) =>
        _questionsSservice = questionsService;

    // GET: api/Questions
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Question>> GetQuestion() =>
        await _questionsSservice.GetQuestionAsync();

    // GET: api/Questions/5
    [HttpGet("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Question>> GetQuestion(string id)
    {
        if (id.Length <= 23 || id.Length >= 25 || id == null)
        {
            return BadRequest();
        }
        var question = await _questionsSservice.GetQuestionAsync(id);
        return question != null ? Ok(question) : NotFound();
    }

    // POST: api/Questions
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> PostQuestion(Question newQuestion)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await _questionsSservice.CreateQuestionAsync(newQuestion);

        return CreatedAtAction(nameof(GetQuestion), new { id = newQuestion.Id }, newQuestion);
    }

    // Put: api/Questions/5
    [HttpPut("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateQuestion(string id, Question updatedQuestion)
    {
        if (!ModelState.IsValid || id.Length <= 23 || id.Length >= 25 || id == null)
        {
            return BadRequest();
        }
        var game = await _questionsSservice.GetQuestionAsync(id);

        if (game is null)
        {
            return NotFound();
        }

        updatedQuestion.Id = game.Id;

        await _questionsSservice.UpdateQuestionAsync(id, updatedQuestion);

        return NoContent();
    }

    // Delete: api/Questions/5
    [HttpDelete("{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteQuestion(string id)
    {
        if (id.Length <= 23 || id.Length >= 25 || id == null)
        {
            return BadRequest();
        }

        var game = await _questionsSservice.GetQuestionAsync(id);

        if (game is null)
        {
            return NotFound();
        }

        await _questionsSservice.DeleteQuestionAsync(id);

        return NoContent();
    }
}
