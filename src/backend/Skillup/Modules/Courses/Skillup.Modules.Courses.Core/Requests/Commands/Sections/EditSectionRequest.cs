﻿
using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Sections
{
    public record EditSectionRequest(string Title, bool IsPublished) : IRequest
    {
        [JsonIgnore]
        public Guid SectionId { get; set; }
    }
}
