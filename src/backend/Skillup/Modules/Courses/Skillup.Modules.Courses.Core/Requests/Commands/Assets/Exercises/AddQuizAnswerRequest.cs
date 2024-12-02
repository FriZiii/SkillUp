﻿using MediatR;
using Skillup.Modules.Courses.Core.DTO.Assets.Exercises;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Assets.Exercises
{
    public record AddQuizAnswerRequest(string Answer, bool IsCorrect) : IRequest<QuizAnswerDto>
    {
        [JsonIgnore]
        public Guid QuizId;
    }
}
