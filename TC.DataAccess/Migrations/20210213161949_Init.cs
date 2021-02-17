using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TC.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "project");

            migrationBuilder.EnsureSchema(
                name: "test");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "ConfigProjectTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigProjectTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screenshot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenshot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModel_UserModel_MasterId",
                        column: x => x.MasterId,
                        principalSchema: "user",
                        principalTable: "UserModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProjectStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDomain",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDomain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDomain_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTestRunConfig",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ConfigProjectTestId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTestRunConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTestRunConfig_ConfigProjectTest_ConfigProjectTestId",
                        column: x => x.ConfigProjectTestId,
                        principalTable: "ConfigProjectTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTestRunConfig_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestInfo",
                schema: "test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    SeleniumCommands = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestInfo_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInProject",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<int>(type: "int", nullable: false),
                    UserProjectStatusId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInProject_UserModel_UserModelId",
                        column: x => x.UserModelId,
                        principalSchema: "user",
                        principalTable: "UserModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInProject_UserProjectStatus_UserProjectStatusId",
                        column: x => x.UserProjectStatusId,
                        principalTable: "UserProjectStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestInfoConfig",
                schema: "test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestInfoId = table.Column<int>(type: "int", nullable: false),
                    ConfigProjectTestId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInfoConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestInfoConfig_ConfigProjectTest_ConfigProjectTestId",
                        column: x => x.ConfigProjectTestId,
                        principalTable: "ConfigProjectTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestInfoConfig_TestInfo_TestInfoId",
                        column: x => x.TestInfoId,
                        principalSchema: "test",
                        principalTable: "TestInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestRunHistory",
                schema: "test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestInfoId = table.Column<int>(type: "int", nullable: false),
                    SelectedBrowserEngine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRunHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRunHistory_TestInfo_TestInfoId",
                        column: x => x.TestInfoId,
                        principalSchema: "test",
                        principalTable: "TestInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestRunResult",
                schema: "test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandTestGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunTime = table.Column<int>(type: "int", nullable: false),
                    TestRunHistoryId = table.Column<int>(type: "int", nullable: false),
                    ScreenshotId = table.Column<int>(type: "int", nullable: true),
                    IsSuccesful = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRunResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRunResult_Screenshot_ScreenshotId",
                        column: x => x.ScreenshotId,
                        principalTable: "Screenshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestRunResult_TestRunHistory_TestRunHistoryId",
                        column: x => x.TestRunHistoryId,
                        principalSchema: "test",
                        principalTable: "TestRunHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConfigProjectTest",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "DefaultValue", "Description", "IsActive", "ModifiedBy", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2021, 2, 13, 10, 19, 48, 94, DateTimeKind.Local).AddTicks(9214), new DateTime(2021, 2, 13, 10, 19, 48, 95, DateTimeKind.Local).AddTicks(272), "false", "It will take a screenshot after every command.", true, "system", "Take Screenshot After Every Command", 0 },
                    { 2, "system", new DateTime(2021, 2, 13, 10, 19, 48, 95, DateTimeKind.Local).AddTicks(1982), new DateTime(2021, 2, 13, 10, 19, 48, 95, DateTimeKind.Local).AddTicks(2007), "false", "Test will monitor every http call.", true, "system", "Monitoring Http Calls", 0 },
                    { 3, "system", new DateTime(2021, 2, 13, 10, 19, 48, 95, DateTimeKind.Local).AddTicks(2180), new DateTime(2021, 2, 13, 10, 19, 48, 95, DateTimeKind.Local).AddTicks(2192), "true", "Test will continue even if a command will fail.", true, "system", "Continue After Command Failure", 0 },
                    { 4, "system", new DateTime(2021, 2, 13, 10, 19, 48, 95, DateTimeKind.Local).AddTicks(2280), new DateTime(2021, 2, 13, 10, 19, 48, 95, DateTimeKind.Local).AddTicks(2292), "true", "Every step is waiting for all network e.g. api xhr, images,etc. to finish before go to next command", true, "system", "Wait for network call finish", 0 }
                });

            migrationBuilder.InsertData(
                table: "UserProjectStatus",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(8332), new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(9186), true, "system", "Pending" },
                    { 2, "system", new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(9861), new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(9880), true, "system", "Accepted" },
                    { 3, "system", new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(9885), new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(9887), true, "system", "Rejected" },
                    { 4, "system", new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(9892), new DateTime(2021, 2, 13, 10, 19, 48, 89, DateTimeKind.Local).AddTicks(9895), true, "system", "Deleted" }
                });

            migrationBuilder.InsertData(
                schema: "project",
                table: "Project",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name" },
                values: new object[] { 1, "system", new DateTime(2021, 2, 13, 10, 19, 48, 90, DateTimeKind.Local).AddTicks(5557), new DateTime(2021, 2, 13, 10, 19, 48, 90, DateTimeKind.Local).AddTicks(6266), "Google", true, "system", "Google" });

            migrationBuilder.InsertData(
                schema: "user",
                table: "UserModel",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedBy", "DateAdded", "DateModified", "Email", "EmailConfirmed", "Guid", "IsActive", "LockoutEnabled", "LockoutEnd", "MasterId", "ModifiedBy", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "d79c807a-edd1-423e-b5dd-d6991ef5ccb1", "system", new DateTime(2021, 2, 13, 10, 19, 48, 81, DateTimeKind.Local).AddTicks(4103), new DateTime(2021, 2, 13, 10, 19, 48, 86, DateTimeKind.Local).AddTicks(6640), "test@test", false, new Guid("ae1ba3dd-63e2-4273-abd5-4010640603c4"), true, false, null, null, "system", "tester", null, null, "5lWUcAhyHCV2rTZqqyE8JIMZJjjAwlwRMrq5jxH+KQY=", null, null, false, "7369c970-f989-4b5a-8eeb-6053b869bc75", false, "user name" });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectDomain",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Domain", "IsActive", "ModifiedBy", "ProjectId" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2021, 2, 13, 10, 19, 48, 92, DateTimeKind.Local).AddTicks(1312), new DateTime(2021, 2, 13, 10, 19, 48, 92, DateTimeKind.Local).AddTicks(2452), "google.com", true, "system", 1 },
                    { 2, "system", new DateTime(2021, 2, 13, 10, 19, 48, 92, DateTimeKind.Local).AddTicks(3968), new DateTime(2021, 2, 13, 10, 19, 48, 92, DateTimeKind.Local).AddTicks(3998), "google.pl", true, "system", 1 }
                });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectTestRunConfig",
                columns: new[] { "Id", "ConfigProjectTestId", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "ProjectId", "Value" },
                values: new object[] { 1, 1, "system", new DateTime(2021, 2, 13, 10, 19, 48, 96, DateTimeKind.Local).AddTicks(1263), new DateTime(2021, 2, 13, 10, 19, 48, 96, DateTimeKind.Local).AddTicks(2370), true, "system", 1, "true" });

            migrationBuilder.InsertData(
                schema: "test",
                table: "TestInfo",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name", "ProjectId", "SeleniumCommands" },
                values: new object[] { 1, "system", new DateTime(2021, 2, 13, 10, 19, 48, 93, DateTimeKind.Local).AddTicks(6647), new DateTime(2021, 2, 13, 10, 19, 48, 93, DateTimeKind.Local).AddTicks(7548), "Using google search find c# tutorial", true, "system", "Search for c# tutorial", 1, "[{\"OperationId\":3,\"WebDriverOperationType\":0,\"Values\":[\"https://www.google.com\"]}]" });

            migrationBuilder.InsertData(
                schema: "user",
                table: "UserInProject",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "ProjectId", "UserModelId", "UserProjectStatusId" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 1, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDomain_ProjectId",
                schema: "project",
                table: "ProjectDomain",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTestRunConfig_ConfigProjectTestId",
                schema: "project",
                table: "ProjectTestRunConfig",
                column: "ConfigProjectTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTestRunConfig_ProjectId",
                schema: "project",
                table: "ProjectTestRunConfig",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInfo_ProjectId",
                schema: "test",
                table: "TestInfo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInfoConfig_ConfigProjectTestId",
                schema: "test",
                table: "TestInfoConfig",
                column: "ConfigProjectTestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestInfoConfig_TestInfoId",
                schema: "test",
                table: "TestInfoConfig",
                column: "TestInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRunHistory_TestInfoId",
                schema: "test",
                table: "TestRunHistory",
                column: "TestInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRunResult_ScreenshotId",
                schema: "test",
                table: "TestRunResult",
                column: "ScreenshotId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRunResult_TestRunHistoryId",
                schema: "test",
                table: "TestRunResult",
                column: "TestRunHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInProject_ProjectId",
                schema: "user",
                table: "UserInProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInProject_UserModelId",
                schema: "user",
                table: "UserInProject",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInProject_UserProjectStatusId",
                schema: "user",
                table: "UserInProject",
                column: "UserProjectStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_MasterId",
                schema: "user",
                table: "UserModel",
                column: "MasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectDomain",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ProjectTestRunConfig",
                schema: "project");

            migrationBuilder.DropTable(
                name: "TestInfoConfig",
                schema: "test");

            migrationBuilder.DropTable(
                name: "TestRunResult",
                schema: "test");

            migrationBuilder.DropTable(
                name: "UserInProject",
                schema: "user");

            migrationBuilder.DropTable(
                name: "ConfigProjectTest");

            migrationBuilder.DropTable(
                name: "Screenshot");

            migrationBuilder.DropTable(
                name: "TestRunHistory",
                schema: "test");

            migrationBuilder.DropTable(
                name: "UserModel",
                schema: "user");

            migrationBuilder.DropTable(
                name: "UserProjectStatus");

            migrationBuilder.DropTable(
                name: "TestInfo",
                schema: "test");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "project");
        }
    }
}
