﻿using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class EditCourseHandler : IRequestHandler<EditCourseRequest, CourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<EditCourseHandler> _logger;
        private readonly IAmazonS3Service _amazonS3Service;

        public EditCourseHandler(ICourseRepository courseRepository, IUserRepository userRepository, ILogger<EditCourseHandler> logger, IAmazonS3Service amazonS3Service)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _logger = logger;
            _amazonS3Service = amazonS3Service;
        }
        public async Task<CourseDto> Handle(EditCourseRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseID) ?? throw new NotFoundException($"Course with ID {request.CourseID} not found");
            course.Title = request.Title;
            course.CategoryId = request.CategoryId;
            course.SubcategoryId = request.SubcategoryId;

            await _courseRepository.Edit(course);
            _logger.LogInformation("Element edited");

            var newCourse = await _courseRepository.GetById(request.CourseID) ?? throw new NotFoundException($"Element with ID {request.CourseID} not found");
            var courseMapper = new CourseMapper(_amazonS3Service);
            var courseDto = courseMapper.CourseToCourseDto(newCourse);
            var user = await _userRepository.GetById(courseDto.AuthorId) ?? throw new NotFoundException($"User with ID {courseDto.AuthorId} not found");
            courseDto.AuthorName = user.FirstName + " " + user.LastName;
            return courseDto;
        }
    }
}
