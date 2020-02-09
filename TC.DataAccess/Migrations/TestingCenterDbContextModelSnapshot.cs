﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TC.DataAccess.DatabaseContext;

namespace TC.DataAccess.Migrations
{
    [DbContext(typeof(TestingCenterDbContext))]
    partial class TestingCenterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TC.Entity.Entities.ConfigProjectTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ConfigProjectTest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(7455),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(8016),
                            DefaultValue = "false",
                            Description = "It will take a screenshot after every command.",
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Take Screenshot After Every Command",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(8931),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(8958),
                            DefaultValue = "false",
                            Description = "Test will monitor every http call.",
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Monitoring Http Calls",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(9015),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(9022),
                            DefaultValue = "true",
                            Description = "Test will continue even if a command will fail.",
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Continue After Command Failure",
                            Type = 0
                        });
                });

            modelBuilder.Entity("TC.Entity.Entities.ProjectDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Domain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectDomain","project");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 873, DateTimeKind.Local).AddTicks(3309),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 873, DateTimeKind.Local).AddTicks(3884),
                            Domain = "google.com",
                            IsActive = true,
                            ModifiedBy = "system",
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 873, DateTimeKind.Local).AddTicks(4743),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 873, DateTimeKind.Local).AddTicks(4773),
                            Domain = "google.pl",
                            IsActive = true,
                            ModifiedBy = "system",
                            ProjectId = 1
                        });
                });

            modelBuilder.Entity("TC.Entity.Entities.ProjectTestConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConfigProjectTestId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigProjectTestId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTestRunConfig","project");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConfigProjectTestId = 1,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 875, DateTimeKind.Local).AddTicks(3151),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 875, DateTimeKind.Local).AddTicks(3697),
                            IsActive = true,
                            ModifiedBy = "system",
                            ProjectId = 1,
                            Value = "true"
                        });
                });

            modelBuilder.Entity("TC.Entity.Entities.Projects.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Project","project");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 872, DateTimeKind.Local).AddTicks(8278),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 872, DateTimeKind.Local).AddTicks(8887),
                            Description = "",
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Google"
                        });
                });

            modelBuilder.Entity("TC.Entity.Entities.Screenshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Screenshot");
                });

            modelBuilder.Entity("TC.Entity.Entities.TestInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("SeleniumCommands")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("TestInfo","test");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(1593),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 874, DateTimeKind.Local).AddTicks(2160),
                            Description = "Using google search find c# tutorial",
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Search for c# tutorial",
                            ProjectId = 1,
                            SeleniumCommands = "[{\"OperationId\":3,\"WebDriverOperationType\":0,\"Values\":[\"https://www.google.com\"]}]"
                        });
                });

            modelBuilder.Entity("TC.Entity.Entities.TestInfoConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConfigProjectTestId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TestInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigProjectTestId");

                    b.HasIndex("TestInfoId");

                    b.ToTable("TestInfoConfig","test");
                });

            modelBuilder.Entity("TC.Entity.Entities.TestRunHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TestInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestInfoId");

                    b.ToTable("TestRunHistory","test");
                });

            modelBuilder.Entity("TC.Entity.Entities.TestRunResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommandTestGuid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuccesful")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RunTime")
                        .HasColumnType("int");

                    b.Property<int?>("ScreenshotId")
                        .HasColumnType("int");

                    b.Property<int?>("TestRunHistoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScreenshotId");

                    b.HasIndex("TestRunHistoryId");

                    b.ToTable("TestRunResult","test");
                });

            modelBuilder.Entity("TC.Entity.Entities.User.UserProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserProjectStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 868, DateTimeKind.Local).AddTicks(1660),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 870, DateTimeKind.Local).AddTicks(8486),
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 870, DateTimeKind.Local).AddTicks(9261),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 870, DateTimeKind.Local).AddTicks(9290),
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 870, DateTimeKind.Local).AddTicks(9305),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 870, DateTimeKind.Local).AddTicks(9309),
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 4,
                            CreatedBy = "system",
                            DateAdded = new DateTime(2020, 2, 7, 9, 20, 57, 870, DateTimeKind.Local).AddTicks(9313),
                            DateModified = new DateTime(2020, 2, 7, 9, 20, 57, 870, DateTimeKind.Local).AddTicks(9317),
                            IsActive = true,
                            ModifiedBy = "system",
                            Name = "Deleted"
                        });
                });

            modelBuilder.Entity("TC.Entity.Entities.UserInProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserModelId")
                        .HasColumnType("int");

                    b.Property<int>("UserProjectStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserModelId");

                    b.HasIndex("UserProjectStatusId");

                    b.ToTable("UserInProject","user");
                });

            modelBuilder.Entity("TC.Entity.Entities.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("MasterId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("UserModel","user");
                });

            modelBuilder.Entity("TC.Entity.Entities.ProjectDomain", b =>
                {
                    b.HasOne("TC.Entity.Entities.Projects.Project", "Project")
                        .WithMany("ProjectDomains")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TC.Entity.Entities.ProjectTestConfig", b =>
                {
                    b.HasOne("TC.Entity.Entities.ConfigProjectTest", "ConfigProjectTest")
                        .WithMany()
                        .HasForeignKey("ConfigProjectTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TC.Entity.Entities.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TC.Entity.Entities.TestInfo", b =>
                {
                    b.HasOne("TC.Entity.Entities.Projects.Project", "Project")
                        .WithMany("TestInfos")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TC.Entity.Entities.TestInfoConfig", b =>
                {
                    b.HasOne("TC.Entity.Entities.ConfigProjectTest", "ConfigProjectTest")
                        .WithMany()
                        .HasForeignKey("ConfigProjectTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TC.Entity.Entities.TestInfo", "Project")
                        .WithMany()
                        .HasForeignKey("TestInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TC.Entity.Entities.TestRunHistory", b =>
                {
                    b.HasOne("TC.Entity.Entities.TestInfo", "TestInfo")
                        .WithMany("TestRunHistory")
                        .HasForeignKey("TestInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TC.Entity.Entities.TestRunResult", b =>
                {
                    b.HasOne("TC.Entity.Entities.Screenshot", "Screenshot")
                        .WithMany()
                        .HasForeignKey("ScreenshotId");

                    b.HasOne("TC.Entity.Entities.TestRunHistory", null)
                        .WithMany("TestRunResults")
                        .HasForeignKey("TestRunHistoryId");
                });

            modelBuilder.Entity("TC.Entity.Entities.UserInProject", b =>
                {
                    b.HasOne("TC.Entity.Entities.Projects.Project", "Project")
                        .WithMany("UserInProject")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TC.Entity.Entities.UserModel", "UserModel")
                        .WithMany()
                        .HasForeignKey("UserModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TC.Entity.Entities.User.UserProjectStatus", "UserProjectStatus")
                        .WithMany()
                        .HasForeignKey("UserProjectStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TC.Entity.Entities.UserModel", b =>
                {
                    b.HasOne("TC.Entity.Entities.UserModel", "Master")
                        .WithMany()
                        .HasForeignKey("MasterId");
                });
#pragma warning restore 612, 618
        }
    }
}
