using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ExercisesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("question/{assignmentId}")]
        [SwaggerOperation("Add question")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuestion(Guid assignmentId, string question, string answer)
        {
            var exercise = await _mediator.Send(new AddQuestionAnswerRequest(assignmentId, question, answer));
            return Ok(exercise);
        }

        [HttpPost("quiz/{assignmentId}")]
        [SwaggerOperation("Add question")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuiz(Guid assignmentId, string question)
        {
            var exercise = await _mediator.Send(new AddQuizQuestionRequest(assignmentId, question));
            return Ok(exercise);
        }

        [HttpPost("quizanswer/{quizId}")]
        [SwaggerOperation("Add question")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuizAnswer(Guid quizId, string answer, bool isCorrect)
        {
            var newAnswer = await _mediator.Send(new AddQuizAnswerRequest(quizId, answer, isCorrect));
            return Ok(newAnswer);
        }
    }
}
