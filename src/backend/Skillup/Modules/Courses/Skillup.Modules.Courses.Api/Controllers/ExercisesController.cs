using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets.Exercise;
using Skillup.Shared.Abstractions.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ExercisesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = nameof(UserRole.Instructor))]
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

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPost("Quiz/{assignmentId}")]
        [SwaggerOperation("Add quiz")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuiz(Guid assignmentId, AddQuizQuestionRequest request)
        {
            request.AssignmentId = assignmentId;
            var exercise = await _mediator.Send(request);
            return Ok(exercise);
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPost("FillTheGap/{assignmentId}")]
        [SwaggerOperation("Add fill the gap")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFillTheGap(Guid assignmentId, AddFillTheGapRequest request)
        {
            request.AssignmentId = assignmentId;
            var exercise = await _mediator.Send(request);
            return Ok(exercise);
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
                ExerciseType.FillTheGap => Ok(await _mediator.Send(new GetFillTheGapListRequest(assignmentId))),
                _ => BadRequest(),
            };
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpDelete("{exerciseType}/{exerciseId}")]
        [SwaggerOperation("Delete exercise")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteExercise(Guid exerciseId, [FromRoute] ExerciseType exerciseType)
        {
            var request = new DeleteExerciseRequest(exerciseId, exerciseType);
            await _mediator.Send(request);
            return Ok();
        }
    }
}
