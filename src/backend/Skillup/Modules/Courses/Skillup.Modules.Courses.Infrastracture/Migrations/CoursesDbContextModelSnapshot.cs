﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Skillup.Modules.Courses.Infrastracture;

#nullable disable

namespace Skillup.Modules.Courses.Infrastracture.Migrations
{
    [DbContext(typeof(CoursesDbContext))]
    partial class CoursesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("courses")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SubcategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Courses", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ElementId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ElementId")
                        .IsUnique();

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.QuizAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<bool>("isCorrectAnswer")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuizAnswer", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ElementId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Key")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.ToTable("ElementAttachments", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Element", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AssetType")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFree")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Elements", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Sections", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Subcategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.UserEntities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePictureKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Users", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.UserEntities.UserPurchasedCourse", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "CourseId");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId", "CourseId")
                        .IsUnique();

                    b.ToTable("UsersPurchasedCourses", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Article", b =>
                {
                    b.HasBaseType("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Asset");

                    b.Property<Guid>("Key")
                        .HasColumnType("uuid");

                    b.ToTable("ArticleAssets", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Assignment", b =>
                {
                    b.HasBaseType("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Asset");

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("AssignmentAssets", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Video", b =>
                {
                    b.HasBaseType("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Asset");

                    b.Property<Guid>("Key")
                        .HasColumnType("uuid");

                    b.ToTable("VideoAssets", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.QuestionAnswer", b =>
                {
                    b.HasBaseType("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.Exercise");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("QuestionAnswerExercise", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.QuizQuestion", b =>
                {
                    b.HasBaseType("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.Exercise");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("QuizQuestionExercise", "courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Course", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.UserEntities.User", "Author")
                        .WithMany("CreatedCoures")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.Subcategory", "Subcategory")
                        .WithMany("Courses")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseDetails", "Details", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description");

                            b1.Property<string>("IntendedFor")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("IntendedFor");

                            b1.Property<string>("Level")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Difficulty");

                            b1.Property<string>("MustKnowBefore")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("MustKnowBefore");

                            b1.Property<string>("ObjectivesSummary")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ObjectivesSummary");

                            b1.Property<string>("Subtitle")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ThumbnailKey")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ThumbnailKey");

                            b1.HasKey("CourseId");

                            b1.ToTable("Courses", "courses");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("Details")
                        .IsRequired();

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Asset", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Element", "Element")
                        .WithOne("Asset")
                        .HasForeignKey("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Asset", "ElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Element");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.Exercise", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Assignment", "Assignment")
                        .WithMany("Exercises")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.QuizAnswer", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.QuizQuestion", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Attachment", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Element", "Element")
                        .WithMany("Attachments")
                        .HasForeignKey("ElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Element");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Element", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Section", "Section")
                        .WithMany("Elements")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Section", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Subcategory", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.UserEntities.User", b =>
                {
                    b.OwnsOne("Skillup.Modules.Courses.Core.Entities.UserEntities.PrivacySettings", "PrivacySettings", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<bool>("IsAccountPublicForLoggedInUsers")
                                .HasColumnType("boolean")
                                .HasColumnName("IsAccountPublicForLoggedInUsers");

                            b1.Property<bool>("ShowCoursesOnUserProfile")
                                .HasColumnType("boolean")
                                .HasColumnName("ShowCoursesOnUserProfile");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "courses");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Skillup.Modules.Courses.Core.Entities.UserEntities.SocialMediaLinks", "SocialMediaLinks", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Facebook")
                                .HasColumnType("text")
                                .HasColumnName("Facebook");

                            b1.Property<string>("LinkedIn")
                                .HasColumnType("text")
                                .HasColumnName("LinkedIn");

                            b1.Property<string>("Twitter")
                                .HasColumnType("text")
                                .HasColumnName("Twitter");

                            b1.Property<string>("Website")
                                .HasColumnType("text")
                                .HasColumnName("Website");

                            b1.Property<string>("YouTube")
                                .HasColumnType("text")
                                .HasColumnName("YouTube");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "courses");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Skillup.Modules.Courses.Core.Entities.UserEntities.UserDetails", "Details", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Biography")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Biography");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "courses");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Details")
                        .IsRequired();

                    b.Navigation("PrivacySettings")
                        .IsRequired();

                    b.Navigation("SocialMediaLinks")
                        .IsRequired();
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.UserEntities.UserPurchasedCourse", b =>
                {
                    b.HasOne("Skillup.Modules.Courses.Core.Entities.CourseEntities.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skillup.Modules.Courses.Core.Entities.UserEntities.User", null)
                        .WithMany("PurchasedCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Category", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Course", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Element", b =>
                {
                    b.Navigation("Asset")
                        .IsRequired();

                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Section", b =>
                {
                    b.Navigation("Elements");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.Subcategory", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.UserEntities.User", b =>
                {
                    b.Navigation("CreatedCoures");

                    b.Navigation("PurchasedCourses");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Assignment", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises.QuizQuestion", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
