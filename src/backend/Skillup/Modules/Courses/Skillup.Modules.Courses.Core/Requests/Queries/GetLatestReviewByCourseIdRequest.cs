﻿using MediatR;
using Skillup.Modules.Courses.Core.DTO.Review;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetLatestReviewByCourseIdRequest(Guid CourseId) : IRequest<CourseReviewDto>;
}