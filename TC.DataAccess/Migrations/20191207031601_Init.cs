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
                name: "UserGroup",
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
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
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
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
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
                name: "UserInGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserModelId = table.Column<int>(nullable: false),
                    UserGroupId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInGroup_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInGroup_UserModel_UserModelId",
                        column: x => x.UserModelId,
                        principalSchema: "user",
                        principalTable: "UserModel",
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

            migrationBuilder.InsertData(
                table: "UserProjectStatus",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "system", new DateTime(2019, 12, 6, 21, 16, 1, 386, DateTimeKind.Local).AddTicks(1097), new DateTime(2019, 12, 6, 21, 16, 1, 388, DateTimeKind.Local).AddTicks(3633), true, "system", "Pending" },
                    { 2, "system", new DateTime(2019, 12, 6, 21, 16, 1, 388, DateTimeKind.Local).AddTicks(4175), new DateTime(2019, 12, 6, 21, 16, 1, 388, DateTimeKind.Local).AddTicks(4193), true, "system", "Accepted" },
                    { 3, "system", new DateTime(2019, 12, 6, 21, 16, 1, 388, DateTimeKind.Local).AddTicks(4202), new DateTime(2019, 12, 6, 21, 16, 1, 388, DateTimeKind.Local).AddTicks(4205), true, "system", "Rejected" },
                    { 4, "system", new DateTime(2019, 12, 6, 21, 16, 1, 388, DateTimeKind.Local).AddTicks(4207), new DateTime(2019, 12, 6, 21, 16, 1, 388, DateTimeKind.Local).AddTicks(4210), true, "system", "Deleted" }
                });

            migrationBuilder.InsertData(
                schema: "project",
                table: "Project",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name" },
                values: new object[] { 1, "system", new DateTime(2019, 12, 6, 21, 16, 1, 389, DateTimeKind.Local).AddTicks(7287), new DateTime(2019, 12, 6, 21, 16, 1, 389, DateTimeKind.Local).AddTicks(7720), "", true, "system", "Google" });

            migrationBuilder.InsertData(
                table: "TestInfo",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Description", "IsActive", "ModifiedBy", "Name", "ProjectId", "SeleniumCommands" },
                values: new object[] { 1, "system", new DateTime(2019, 12, 6, 21, 16, 1, 390, DateTimeKind.Local).AddTicks(6745), new DateTime(2019, 12, 6, 21, 16, 1, 390, DateTimeKind.Local).AddTicks(7139), "Using google search find c# tutorial", true, "system", "Search for c# tutorial", 1, "[{\"OperationId\":3,\"WebDriverOperationType\":0,\"Values\":[\"https://www.google.com\"]}]" });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectDomain",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Domain", "IsActive", "ModifiedBy", "ProjectId" },
                values: new object[] { 2, "system", new DateTime(2019, 12, 6, 21, 16, 1, 390, DateTimeKind.Local).AddTicks(1944), new DateTime(2019, 12, 6, 21, 16, 1, 390, DateTimeKind.Local).AddTicks(1965), "google.pl", true, "system", 1 });

            migrationBuilder.InsertData(
                schema: "project",
                table: "ProjectDomain",
                columns: new[] { "Id", "CreatedBy", "DateAdded", "DateModified", "Domain", "IsActive", "ModifiedBy", "ProjectId" },
                values: new object[] { 1, "system", new DateTime(2019, 12, 6, 21, 16, 1, 390, DateTimeKind.Local).AddTicks(915), new DateTime(2019, 12, 6, 21, 16, 1, 390, DateTimeKind.Local).AddTicks(1309), "google.com", true, "system", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_TestInfo_ProjectId",
                table: "TestInfo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroup_UserGroupId",
                table: "UserInGroup",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroup_UserModelId",
                table: "UserInGroup",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDomain_ProjectId",
                schema: "project",
                table: "ProjectDomain",
                column: "ProjectId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestInfo");

            migrationBuilder.DropTable(
                name: "UserInGroup");

            migrationBuilder.DropTable(
                name: "ProjectDomain",
                schema: "project");

            migrationBuilder.DropTable(
                name: "UserInProject",
                schema: "user");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "project");

            migrationBuilder.DropTable(
                name: "UserModel",
                schema: "user");

            migrationBuilder.DropTable(
                name: "UserProjectStatus");
        }
    }
}
