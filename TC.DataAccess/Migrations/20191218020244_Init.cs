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
                name: "user");

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
                name: "TestRunConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRunConfig", x => x.Id);
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
                    UserModelId = table.Column<int>(nullable: true),
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
                name: "TestInfo",
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
                    TestRunConfigId = table.Column<int>(nullable: false),
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
                        name: "FK_ProjectTestRunConfig_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTestRunConfig_TestRunConfig_TestRunConfigId",
                        column: x => x.TestRunConfigId,
                        principalTable: "TestRunConfig",
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
                name: "TestRunHistory",
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
                        principalTable: "TestInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestRunRestult",
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
                    table.PrimaryKey("PK_TestRunRestult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRunRestult_Screenshot_ScreenshotId",
                        column: x => x.ScreenshotId,
                        principalTable: "Screenshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestRunRestult_TestRunHistory_TestRunHistoryId",
                        column: x => x.TestRunHistoryId,
                        principalTable: "TestRunHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TestRunConfig",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(388), new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(756), "It will take a screenshot after every command.", true, "system", "Take Screenshot After Every Command", 0, null },
                    { 2, "system", new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(1344), new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(1361), "Test will monitor every http call.", true, "system", "Monitoring Http Calls", 0, null },
                    { 3, "system", new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(1394), new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(1396), "Test will continue even if a command will fail.", true, "system", "Continue After Command Failure", 0, null }
                });

            migrationBuilder.InsertData(
                table: "UserProjectStatus",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2019, 12, 17, 20, 2, 43, 945, DateTimeKind.Local).AddTicks(5192), new DateTime(2019, 12, 17, 20, 2, 43, 947, DateTimeKind.Local).AddTicks(3992), true, "system", "Pending" },
                    { 2, "system", new DateTime(2019, 12, 17, 20, 2, 43, 947, DateTimeKind.Local).AddTicks(4506), new DateTime(2019, 12, 17, 20, 2, 43, 947, DateTimeKind.Local).AddTicks(4523), true, "system", "Accepted" },
                    { 3, "system", new DateTime(2019, 12, 17, 20, 2, 43, 947, DateTimeKind.Local).AddTicks(4531), new DateTime(2019, 12, 17, 20, 2, 43, 947, DateTimeKind.Local).AddTicks(4534), true, "system", "Rejected" },
                    { 4, "system", new DateTime(2019, 12, 17, 20, 2, 43, 947, DateTimeKind.Local).AddTicks(4537), new DateTime(2019, 12, 17, 20, 2, 43, 947, DateTimeKind.Local).AddTicks(4539), true, "system", "Deleted" }
                });

            migrationBuilder.InsertData(
                schema: "project",
                table: "Project",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name" },
                values: new object[] { 1, "system", new DateTime(2019, 12, 17, 20, 2, 43, 948, DateTimeKind.Local).AddTicks(7358), new DateTime(2019, 12, 17, 20, 2, 43, 948, DateTimeKind.Local).AddTicks(7753), "", true, "system", "Google" });

            migrationBuilder.InsertData(
                table: "TestInfo",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name", "ProjectId", "SeleniumCommands" },
                values: new object[] { 1, "system", new DateTime(2019, 12, 17, 20, 2, 43, 949, DateTimeKind.Local).AddTicks(6599), new DateTime(2019, 12, 17, 20, 2, 43, 949, DateTimeKind.Local).AddTicks(6973), "Using google search find c# tutorial", true, "system", "Search for c# tutorial", 1, "[{\"OperationId\":3,\"WebDriverOperationType\":0,\"Values\":[\"https://www.google.com\"]}]" });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectDomain",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Domain", "IsActive", "ModifiedBy", "ProjectId" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2019, 12, 17, 20, 2, 43, 949, DateTimeKind.Local).AddTicks(1130), new DateTime(2019, 12, 17, 20, 2, 43, 949, DateTimeKind.Local).AddTicks(1507), "google.com", true, "system", 1 },
                    { 2, "system", new DateTime(2019, 12, 17, 20, 2, 43, 949, DateTimeKind.Local).AddTicks(2092), new DateTime(2019, 12, 17, 20, 2, 43, 949, DateTimeKind.Local).AddTicks(2110), "google.pl", true, "system", 1 }
                });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectTestRunConfig",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "ProjectId", "TestRunConfigId", "Value" },
                values: new object[] { 1, "system", new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(4137), new DateTime(2019, 12, 17, 20, 2, 43, 950, DateTimeKind.Local).AddTicks(4505), true, "system", 1, 1, "true" });

            migrationBuilder.CreateIndex(
                name: "IX_TestInfo_ProjectId",
                table: "TestInfo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRunHistory_TestInfoId",
                table: "TestRunHistory",
                column: "TestInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestRunRestult_ScreenshotId",
                table: "TestRunRestult",
                column: "ScreenshotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestRunRestult_TestRunHistoryId",
                table: "TestRunRestult",
                column: "TestRunHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDomain_ProjectId",
                schema: "project",
                table: "ProjectDomain",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTestRunConfig_ProjectId",
                schema: "project",
                table: "ProjectTestRunConfig",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTestRunConfig_TestRunConfigId",
                schema: "project",
                table: "ProjectTestRunConfig",
                column: "TestRunConfigId");

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
                name: "TestRunRestult");

            migrationBuilder.DropTable(
                name: "ProjectDomain",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ProjectTestRunConfig",
                schema: "project");

            migrationBuilder.DropTable(
                name: "UserInProject",
                schema: "user");

            migrationBuilder.DropTable(
                name: "Screenshot");

            migrationBuilder.DropTable(
                name: "TestRunHistory");

            migrationBuilder.DropTable(
                name: "TestRunConfig");

            migrationBuilder.DropTable(
                name: "UserModel",
                schema: "user");

            migrationBuilder.DropTable(
                name: "UserProjectStatus");

            migrationBuilder.DropTable(
                name: "TestInfo");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "project");
        }
    }
}
