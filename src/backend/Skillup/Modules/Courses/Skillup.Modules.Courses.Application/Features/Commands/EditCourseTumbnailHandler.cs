using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Features.Commands.Users;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Options;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Shared.Abstractions.S3;
using System.Net;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    internal class EditCourseTumbnailHandler : IRequestHandler<EditCourseTumbnailRequest>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAmazonS3Service _s3Service;
        private readonly ILogger<EditUserProfilePictureHandler> _logger;

        public EditCourseTumbnailHandler(ICourseRepository courseRepository, IAmazonS3Service s3Service, ILogger<EditUserProfilePictureHandler> logger)
        {
            _courseRepository = courseRepository;
            _s3Service = s3Service;
            _logger = logger;
        }

        public async Task Handle(EditCourseTumbnailRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseId) ?? throw new Exception(); // TODO: Custom ex: Course with id doesnt exist

            var key = course.Details.ThumbnailKey;

            if (course.Details.ThumbnailKey == CourseModuleOptions.DefaultValues.DefaultTubnailPictureKey)
            {
                key = Guid.NewGuid().ToString();
            }

            var response = await _s3Service.Upload(request.File, S3FolderPaths.CourseTubnailPicture + key);
            if (response?.HttpStatusCode == HttpStatusCode.OK)
            {
                course.Details.ThumbnailKey = key;
                await _courseRepository.EditDetails(course.Id, course.Details);

                _logger.LogInformation($"Course tumbnail has been changed, CourseId = {course.Id}");
            }
        }
    }
}
