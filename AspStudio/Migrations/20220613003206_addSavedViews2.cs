using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EagleApp.Migrations
{
    public partial class addSavedViews2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "SavedViews",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  ViewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  SavedJsonCriteria = table.Column<string>(type: "nvarchar(max)", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_SavedViews", x => x.Id);
              });
        }

       
       
    }
}
