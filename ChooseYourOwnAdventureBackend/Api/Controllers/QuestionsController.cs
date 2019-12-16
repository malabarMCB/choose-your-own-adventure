using System;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService ?? throw new ArgumentNullException(nameof(questionsService));
        }

        [HttpGet("{id}")]
        public IActionResult GetQuestion(int id)
        {
            var question = _questionsService.GetQuestion(id);

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        [HttpGet("tree")]
        public IActionResult GetQuestionsTree()
        {
            return Ok();
        }
    }
}