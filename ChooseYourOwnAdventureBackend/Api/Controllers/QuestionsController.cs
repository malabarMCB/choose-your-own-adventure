using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult GetQuestion(int id)
        {
            return Ok();
        }

        [HttpGet("tree")]
        public IActionResult GetQuestionsTree()
        {
            return Ok();
        }
    }
}