using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatDogLoverRepository.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "TypeGoods",
                columns: table => new
                {
                    TypeGoodsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeGoodsName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeGoods", x => x.TypeGoodsID);
                });

            migrationBuilder.CreateTable(
                name: "TypeNewsFeeds",
                columns: table => new
                {
                    TypesNewFeedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypesNewFeedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeNewsFeeds", x => x.TypesNewFeedID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Otp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogOutDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeeds",
                columns: table => new
                {
                    NewsFeedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeGoodsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeNewsFeedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeeds", x => x.NewsFeedID);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_TypeGoods_TypeGoodsID",
                        column: x => x.TypeGoodsID,
                        principalTable: "TypeGoods",
                        principalColumn: "TypeGoodsID");
                    table.ForeignKey(
                        name: "FK_NewsFeeds_TypeNewsFeeds_TypeNewsFeedID",
                        column: x => x.TypeNewsFeedID,
                        principalTable: "TypeNewsFeeds",
                        principalColumn: "TypesNewFeedID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsFeeds_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsFeedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_NewsFeeds_NewsFeedID",
                        column: x => x.NewsFeedID,
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedID");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsFeedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Images_NewsFeeds_NewsFeedID",
                        column: x => x.NewsFeedID,
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumberOfInteractions",
                columns: table => new
                {
                    NumberOfInteractionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsFeedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfInteractions", x => x.NumberOfInteractionID);
                    table.ForeignKey(
                        name: "FK_NumberOfInteractions_NewsFeeds_NewsFeedID",
                        column: x => x.NewsFeedID,
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedID");
                    table.ForeignKey(
                        name: "FK_NumberOfInteractions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_NewsFeedID",
                table: "Comments",
                column: "NewsFeedID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsFeedID",
                table: "Images",
                column: "NewsFeedID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_TypeGoodsID",
                table: "NewsFeeds",
                column: "TypeGoodsID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_TypeNewsFeedID",
                table: "NewsFeeds",
                column: "TypeNewsFeedID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_UserID",
                table: "NewsFeeds",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_NumberOfInteractions_NewsFeedID",
                table: "NumberOfInteractions",
                column: "NewsFeedID");

            migrationBuilder.CreateIndex(
                name: "IX_NumberOfInteractions_UserID",
                table: "NumberOfInteractions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "NumberOfInteractions");

            migrationBuilder.DropTable(
                name: "NewsFeeds");

            migrationBuilder.DropTable(
                name: "TypeGoods");

            migrationBuilder.DropTable(
                name: "TypeNewsFeeds");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
