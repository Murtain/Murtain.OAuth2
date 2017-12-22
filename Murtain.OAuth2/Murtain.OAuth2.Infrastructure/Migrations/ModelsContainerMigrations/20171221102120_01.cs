using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Murtain.OAuth2.Infrastructure.Migrations.ModelsContainerMigrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 250, nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 2000, nullable: true),
                    Birthday = table.Column<string>(maxLength: 50, nullable: true),
                    ChangeTime = table.Column<DateTime>(nullable: true),
                    ChangeUser = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<string>(maxLength: 50, nullable: true),
                    IsActived = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 50, nullable: true),
                    LoginProviderId = table.Column<string>(maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    NickName = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 250, nullable: true),
                    Province = table.Column<string>(maxLength: 50, nullable: true),
                    SubjectId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccount");
        }
    }
}
