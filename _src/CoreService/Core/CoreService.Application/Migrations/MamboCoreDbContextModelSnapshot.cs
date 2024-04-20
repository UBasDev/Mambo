﻿// <auto-generated />
using System;
using CoreService.Application.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreService.Application.Migrations
{
    [DbContext(typeof(MamboCoreDbContext))]
    partial class MamboCoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskContentCommentEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<Guid>("CommenterId")
                        .HasColumnType("uuid");

                    b.Property<string>("CommenterName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("TaskContentId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TaskContentId");

                    b.ToTable("TaskContentComments");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskContentEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TaskId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.ToTable("TaskContents");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskDetailEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<Guid>("AssignedUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrentPriority")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PriorityList")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RelatedTaskIdList")
                        .HasColumnType("text");

                    b.Property<Guid>("ReporterUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("StatusList")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TaskId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.ToTable("TaskDetails");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<long>("No")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskFieldEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TaskId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskFields");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.CompanyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Adress")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.ProfileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LastLoginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectEntityUserEntity", b =>
                {
                    b.Property<Guid>("ProjectsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("ProjectsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ProjectEntityUserEntity");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskContentCommentEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.Project.TaskContentEntity", "TaskContent")
                        .WithMany("TaskContentComments")
                        .HasForeignKey("TaskContentId");

                    b.Navigation("TaskContent");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskContentEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.Project.TaskEntity", "Task")
                        .WithOne("TaskContent")
                        .HasForeignKey("CoreService.Domain.AggregateRoots.Project.TaskContentEntity", "TaskId");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskDetailEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.Project.TaskEntity", "Task")
                        .WithOne("TaskDetail")
                        .HasForeignKey("CoreService.Domain.AggregateRoots.Project.TaskDetailEntity", "TaskId");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.Project.ProjectEntity", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskFieldEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.Project.TaskEntity", "Task")
                        .WithMany("TaskFields")
                        .HasForeignKey("TaskId");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.ProfileEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.User.CompanyEntity", "Company")
                        .WithMany("Profiles")
                        .HasForeignKey("CompanyId");

                    b.HasOne("CoreService.Domain.AggregateRoots.User.UserEntity", "User")
                        .WithOne("Profile")
                        .HasForeignKey("CoreService.Domain.AggregateRoots.User.ProfileEntity", "UserId");

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.UserEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.User.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProjectEntityUserEntity", b =>
                {
                    b.HasOne("CoreService.Domain.AggregateRoots.Project.ProjectEntity", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreService.Domain.AggregateRoots.User.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.ProjectEntity", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskContentEntity", b =>
                {
                    b.Navigation("TaskContentComments");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.Project.TaskEntity", b =>
                {
                    b.Navigation("TaskContent");

                    b.Navigation("TaskDetail");

                    b.Navigation("TaskFields");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.CompanyEntity", b =>
                {
                    b.Navigation("Profiles");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CoreService.Domain.AggregateRoots.User.UserEntity", b =>
                {
                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}
