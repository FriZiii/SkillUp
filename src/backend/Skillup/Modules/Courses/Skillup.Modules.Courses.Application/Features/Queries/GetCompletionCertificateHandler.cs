using MediatR;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCompletionCertificateHandler(ICourseRepository courseRepository, IUserRepository userRepository) : IRequestHandler<GetCompletionCertificateRequest, FileDto>
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IUserRepository _userRepository = userRepository;

        [Obsolete]
        public async Task<FileDto> Handle(GetCompletionCertificateRequest request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseId) ?? throw new Exception(); // TODO: Custom ex: course with id doesnt exist
            var user = await _userRepository.GetById(request.UserId) ?? throw new Exception(); // TODO: Custom ex: user with id doesnt exist

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var image = SvgImage.FromFile(Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "logo.svg"));

            var pdf = Document.Create(c =>
            {
                c.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Size(PageSizes.Letter.Landscape());
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(14).FontColor(Colors.Black));
                    page.Content().AlignCenter().Column(column =>
                    {
                        column.Item().Padding(25).AlignCenter().Width(PageSizes.A4.Width / 2).Svg(image).FitArea();

                        column.Item().Text("COURSE COMPLETION").AlignCenter()
                            .FontSize(24)
                            .Light();

                        column.Item().PaddingTop(15).Text("CERTIFICATE").AlignCenter()
                            .FontSize(55)
                            .ExtraBold();

                        column.Item().PaddingTop(25).Text("THIS CERTIFICATE IS PROUDLY PRESENTED TO").AlignCenter()
                            .FontSize(16);

                        column.Item().PaddingTop(15).Text($"{user.FirstName} {user.LastName}").AlignCenter()
                            .FontSize(28).ExtraBold();

                        column.Item().PaddingTop(15).Text($"for successful completion of all the required evaluation process for the course").AlignCenter()
                            .FontSize(14);

                        column.Item().PaddingTop(15).Text($"{course.Title}").AlignCenter()
                            .FontSize(28).ExtraBold();
                    });
                });

            }).GeneratePdf();


            return new FileDto()
            {
                FileName = $"{course.Title}-certificate",
                ContentType = "application/pdf",
                FileData = pdf
            };
        }
    }
}
