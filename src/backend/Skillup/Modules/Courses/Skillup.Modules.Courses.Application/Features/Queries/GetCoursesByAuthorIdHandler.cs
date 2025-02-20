﻿using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCoursesByAuthorIdHandler(ICourseRepository courseRepository, IUserRepository userRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetCoursesByAuthorIdRequest, List<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<List<CourseDto>> Handle(GetCoursesByAuthorIdRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Course?> courses;
            if (request.Status == null)
            {
                courses = await _courseRepository.GetAll();
            }
            else
            {
                courses = await _courseRepository.GetByStatus((CourseStatus)request.Status);
            }
            courses = courses.Where(c => c.AuthorId == request.AuthorId);
            var mapper = new CourseMapper(_amazonS3Service);
            var courseDtos = courses.Select(mapper.CourseToCourseDto).ToList();

            var users = await _userRepository.GetAll();
            courseDtos.ForEach(c => c.AuthorName = users.First(u => u.Id == c.AuthorId).FirstName + " " + users.First(u => u.Id == c.AuthorId).LastName);
            return courseDtos;
        }
    }
}
