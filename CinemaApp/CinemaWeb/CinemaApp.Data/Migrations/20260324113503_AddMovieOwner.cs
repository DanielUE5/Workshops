using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", 0, "d122befb-50d8-4109-93d7-1b0009d771a4", "seeduser@cinemaapp.com", true, false, null, "SEEDUSER@CINEMAAPP.COM", "SEEDUSER@CINEMAAPP.COM", "AQAAAAIAAYagAAAAECCO7lfe3j73vXLpBhxgXweyrR2F4z7eKK9hl3M1pJRFQHBIaVb1Jsr06pxnFeIXgQ==", null, false, "88eb3e59-1504-422d-84a9-12eec087d8f9", false, "seeduser@cinemaapp.com" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("54082f99-023b-4d68-89ac-44c00a0958d0"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("844d9abd-104d-41ab-b14a-ce059779ad91"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ab2c3213-48a7-41ea-9393-45c60ef813e6"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"),
                column: "OwnerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_OwnerId",
                table: "Movies",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_OwnerId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_OwnerId",
                table: "Movies");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Movies");
        }
    }
}
