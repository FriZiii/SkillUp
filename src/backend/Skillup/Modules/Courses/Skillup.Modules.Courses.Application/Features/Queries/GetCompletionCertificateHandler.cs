using MediatR;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetCompletionCertificateHandler : IRequestHandler<GetCompletionCertificateRequest, FileDto>
    {
        [Obsolete]
        public async Task<FileDto> Handle(GetCompletionCertificateRequest request, CancellationToken cancellationToken)
        {
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
                        column.Item().Padding(25).Svg(image).FitArea();

                        column.Item().Text("COURSE COMPLETION").AlignCenter()
                            .FontSize(24)
                            .Light();

                        column.Item().Text("CERTIFICATE").AlignCenter()
                            .FontSize(65)
                            .ExtraBold();

                        column.Item().Text("THIS CERTIFICATE IS PROUDLY PRESENTED TO").AlignCenter()
                            .FontSize(16);

                        column.Item().Text("Małysz Adam").AlignCenter()
                            .FontSize(16).ExtraBold();

                        column.Item().Text($"for successful completion of all the required evaluation process for the course").AlignCenter()
                            .FontSize(14);

                        column.Item().Text("Ruchanie kurwe").AlignCenter()
                            .FontSize(12)
                            .Bold();
                    });
                });

            }).GeneratePdf();


            return new FileDto()
            {
                FileName = "123",
                ContentType = "application/pdf",
                FileData = pdf
            };
        }
    }
}
