﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skillup.Modules.Courses.Core.Entities.CourseContent;

namespace Skillup.Modules.Courses.Infrastracture.Configurations
{
    internal class ElementConfiguration : IEntityTypeConfiguration<Element>
    {
        public void Configure(EntityTypeBuilder<Element> builder)
        {
            builder.HasOne(e => e.Asset)
                .WithOne(a => a.Element)
                .HasForeignKey<Element>(e => e.AssetId);
        }
    }
}