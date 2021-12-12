using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Companys",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Companys", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Logins",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Logins", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "TodoList",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Colour_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TodoList", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Cars",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                HorsePower = table.Column<int>(type: "int", nullable: false),
                YearOfProduction = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CompanyId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cars", x => x.Id);
                table.ForeignKey(
                    name: "FK_Cars_Companys_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "Companys",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Renters",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LicenceObtainmentYear = table.Column<int>(type: "int", nullable: false),
                Age = table.Column<int>(type: "int", nullable: false),
                LoginId = table.Column<int>(type: "int", nullable: false),
                Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Renters", x => x.Id);
                table.ForeignKey(
                    name: "FK_Renters_Logins_LoginId",
                    column: x => x.LoginId,
                    principalTable: "Logins",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Workers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LoginId = table.Column<int>(type: "int", nullable: false),
                CompanyId = table.Column<int>(type: "int", nullable: false),
                Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Workers", x => x.Id);
                table.ForeignKey(
                    name: "FK_Workers_Companys_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "Companys",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Workers_Logins_LoginId",
                    column: x => x.LoginId,
                    principalTable: "Logins",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TodoItem",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ListId = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Priority = table.Column<int>(type: "int", nullable: false),
                Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                Done = table.Column<bool>(type: "bit", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TodoItem", x => x.Id);
                table.ForeignKey(
                    name: "FK_TodoItem_TodoList_ListId",
                    column: x => x.ListId,
                    principalTable: "TodoList",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CarModels",
            columns: table => new
            {
                CarId = table.Column<int>(type: "int", nullable: false),
                Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Model = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CarModels", x => x.CarId);
                table.ForeignKey(
                    name: "FK_CarModels_Cars_CarId",
                    column: x => x.CarId,
                    principalTable: "Cars",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Rents",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                RentStatus = table.Column<int>(type: "int", nullable: false),
                CarId = table.Column<int>(type: "int", nullable: false),
                RenterId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Rents", x => x.Id);
                table.ForeignKey(
                    name: "FK_Rents_Cars_CarId",
                    column: x => x.CarId,
                    principalTable: "Cars",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Rents_Renters_RenterId",
                    column: x => x.RenterId,
                    principalTable: "Renters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Cars_CompanyId",
            table: "Cars",
            column: "CompanyId");

        migrationBuilder.CreateIndex(
            name: "IX_Renters_LoginId",
            table: "Renters",
            column: "LoginId");

        migrationBuilder.CreateIndex(
            name: "IX_Rents_CarId",
            table: "Rents",
            column: "CarId");

        migrationBuilder.CreateIndex(
            name: "IX_Rents_RenterId",
            table: "Rents",
            column: "RenterId");

        migrationBuilder.CreateIndex(
            name: "IX_TodoItem_ListId",
            table: "TodoItem",
            column: "ListId");

        migrationBuilder.CreateIndex(
            name: "IX_Workers_CompanyId",
            table: "Workers",
            column: "CompanyId");

        migrationBuilder.CreateIndex(
            name: "IX_Workers_LoginId",
            table: "Workers",
            column: "LoginId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CarModels");

        migrationBuilder.DropTable(
            name: "Rents");

        migrationBuilder.DropTable(
            name: "TodoItem");

        migrationBuilder.DropTable(
            name: "Workers");

        migrationBuilder.DropTable(
            name: "Cars");

        migrationBuilder.DropTable(
            name: "Renters");

        migrationBuilder.DropTable(
            name: "TodoList");

        migrationBuilder.DropTable(
            name: "Companys");

        migrationBuilder.DropTable(
            name: "Logins");
    }
}
