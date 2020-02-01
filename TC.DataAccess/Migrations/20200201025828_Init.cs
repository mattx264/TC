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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigProjectTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screenshot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenshot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProjectStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MasterId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
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
                name: "ProjectDomain",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    ConfigProjectTestId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    SeleniumCommands = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    UserModelId = table.Column<int>(nullable: false),
                    UserProjectStatusId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestInfoId = table.Column<int>(nullable: false),
                    ConfigProjectTestId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestInfoId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandTestGuid = table.Column<string>(nullable: true),
                    RunTime = table.Column<int>(nullable: false),
                    ScreenshotId = table.Column<int>(nullable: false),
                    IsSuccesful = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    TestRunHistoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRunResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRunResult_Screenshot_ScreenshotId",
                        column: x => x.ScreenshotId,
                        principalTable: "Screenshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestRunResult_TestRunHistory_TestRunHistoryId",
                        column: x => x.TestRunHistoryId,
                        principalSchema: "test",
                        principalTable: "TestRunHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ConfigProjectTest",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "DefaultValue", "Description", "IsActive", "ModifiedBy", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2020, 1, 31, 20, 58, 27, 519, DateTimeKind.Local).AddTicks(2734), new DateTime(2020, 1, 31, 20, 58, 27, 519, DateTimeKind.Local).AddTicks(3549), "false", "It will take a screenshot after every command.", true, "system", "Take Screenshot After Every Command", 0 },
                    { 2, "system", new DateTime(2020, 1, 31, 20, 58, 27, 519, DateTimeKind.Local).AddTicks(5070), new DateTime(2020, 1, 31, 20, 58, 27, 519, DateTimeKind.Local).AddTicks(5113), "false", "Test will monitor every http call.", true, "system", "Monitoring Http Calls", 0 },
                    { 3, "system", new DateTime(2020, 1, 31, 20, 58, 27, 519, DateTimeKind.Local).AddTicks(5226), new DateTime(2020, 1, 31, 20, 58, 27, 519, DateTimeKind.Local).AddTicks(5241), "true", "Test will continue even if a command will fail.", true, "system", "Continue After Command Failure", 0 }
                });

            migrationBuilder.InsertData(
                table: "UserProjectStatus",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2020, 1, 31, 20, 58, 27, 511, DateTimeKind.Local).AddTicks(5676), new DateTime(2020, 1, 31, 20, 58, 27, 514, DateTimeKind.Local).AddTicks(7039), true, "system", "Pending" },
                    { 2, "system", new DateTime(2020, 1, 31, 20, 58, 27, 514, DateTimeKind.Local).AddTicks(7851), new DateTime(2020, 1, 31, 20, 58, 27, 514, DateTimeKind.Local).AddTicks(7882), true, "system", "Accepted" },
                    { 3, "system", new DateTime(2020, 1, 31, 20, 58, 27, 514, DateTimeKind.Local).AddTicks(7896), new DateTime(2020, 1, 31, 20, 58, 27, 514, DateTimeKind.Local).AddTicks(7899), true, "system", "Rejected" },
                    { 4, "system", new DateTime(2020, 1, 31, 20, 58, 27, 514, DateTimeKind.Local).AddTicks(7904), new DateTime(2020, 1, 31, 20, 58, 27, 514, DateTimeKind.Local).AddTicks(7907), true, "system", "Deleted" }
                });

            migrationBuilder.InsertData(
                schema: "project",
                table: "Project",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name" },
                values: new object[] { 1, "system", new DateTime(2020, 1, 31, 20, 58, 27, 516, DateTimeKind.Local).AddTicks(9928), new DateTime(2020, 1, 31, 20, 58, 27, 517, DateTimeKind.Local).AddTicks(616), "", true, "system", "Google" });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectDomain",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Domain", "IsActive", "ModifiedBy", "ProjectId" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2020, 1, 31, 20, 58, 27, 517, DateTimeKind.Local).AddTicks(5356), new DateTime(2020, 1, 31, 20, 58, 27, 517, DateTimeKind.Local).AddTicks(5957), "google.com", true, "system", 1 },
                    { 2, "system", new DateTime(2020, 1, 31, 20, 58, 27, 517, DateTimeKind.Local).AddTicks(6930), new DateTime(2020, 1, 31, 20, 58, 27, 517, DateTimeKind.Local).AddTicks(6960), "google.pl", true, "system", 1 }
                });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectTestRunConfig",
                columns: new[] { "Id", "ConfigProjectTestId", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "ProjectId", "Value" },
                values: new object[] { 1, 1, "system", new DateTime(2020, 1, 31, 20, 58, 27, 520, DateTimeKind.Local).AddTicks(917), new DateTime(2020, 1, 31, 20, 58, 27, 520, DateTimeKind.Local).AddTicks(1519), true, "system", 1, "true" });

            migrationBuilder.InsertData(
                schema: "test",
                table: "TestInfo",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name", "ProjectId", "SeleniumCommands" },
                values: new object[] { 1, "system", new DateTime(2020, 1, 31, 20, 58, 27, 518, DateTimeKind.Local).AddTicks(5813), new DateTime(2020, 1, 31, 20, 58, 27, 518, DateTimeKind.Local).AddTicks(6517), "Using google search find c# tutorial", true, "system", "Search for c# tutorial", 1, "[{\"OperationId\":3,\"WebDriverOperationType\":0,\"Values\":[\"https://www.google.com\"]}]" });

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
                column: "TestInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestRunResult_ScreenshotId",
                schema: "test",
                table: "TestRunResult",
                column: "ScreenshotId",
                unique: true);

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
