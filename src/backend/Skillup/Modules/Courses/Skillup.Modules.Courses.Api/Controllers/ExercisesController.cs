using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets.Exercise;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ExercisesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("Question/{assignmentId}")]
        [SwaggerOperation("Add question")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuestion(Guid assignmentId, AddQuestionAnswerRequest request)
        {
            request.AssignmentId = assignmentId;
            var exercise = await _mediator.Send(request);
            return Ok(exercise);
        }

        [HttpPost("Quiz/{assignmentId}")]
        [SwaggerOperation("Add question")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuiz(Guid assignmentId, AddQuizQuestionRequest request)
        {
            request.AssignmentId = assignmentId;
            var exercise = await _mediator.Send(request);
            return Ok(exercise);
        }

        [HttpPost("QuizAnswer/{quizId}")]
        [SwaggerOperation("Add answer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuizAnswer(Guid quizId, AddQuizAnswerRequest request)
        {

            request.QuizId = quizId;
            var newAnswer = await _mediator.Send(request);
            return Ok(newAnswer);
        }

        [HttpGet("{exerciseType}/{assignmentId}")]
        [SwaggerOperation("Get asset by elementId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAssetByElementId(Guid assignmentId, [FromRoute] ExerciseType exerciseType)
        {
            return exerciseType switch
            {
                ExerciseType.Quiz => Ok(await _mediator.Send(new GetQuizListRequest(assignmentId))),
                ExerciseType.QuestionAnswer => Ok(await _mediator.Send(new GetQuestionAnswerListRequest(assignmentId))),
                ExerciseType.FillTheGap => NotFound(),
                _ => BadRequest(),
            };
        }
    }
}
