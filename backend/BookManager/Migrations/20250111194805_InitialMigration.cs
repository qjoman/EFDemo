using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BooksIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderItemsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => new { x.BookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BookCategories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValideFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookPrices_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BooksIds", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2f6edd3c-4872-46eb-906f-394f09a42360"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "John Doe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("706da7f1-ddc2-4811-9b6b-81a2b9b76667"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Jane Smith", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("80cd7239-2286-4b89-b2ae-6df0a28615f2"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Emily Brown", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a1dd6d4a-4f98-44ab-937a-aa2156e4c728"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "David Johnson", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("05b0ecd6-dc9d-4901-87db-08d23fcd940f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Books based on real-life facts, events, and people.", true, "Non-Fiction", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3fe16924-646b-4341-911b-0e0c1e9a6e29"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Books that tell the life story of real people.", true, "Biography", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("694f18c2-de17-4e1e-9702-d146183803ed"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Books set in imaginary worlds with magic, mythical creatures, and heroic quests.", true, "Fantasy", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("768ddc89-8250-4ec1-a693-42dacebf27ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Books that are intense, suspenseful, and filled with excitement.", true, "Thriller", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a0d2810e-5d51-4fa4-b401-7353a03e582d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Books related to futuristic concepts, space exploration, and advanced technology.", true, "Science Fiction", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CoverImageUrl", "CreatedAt", "Description", "IsActive", "PublishedAt", "Stock", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("460fb0d4-289c-43d9-8055-1fbcf992dcb3"), new Guid("2f6edd3c-4872-46eb-906f-394f09a42360"), "https://example.com/tech_revolution.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An in-depth look at the technological innovations of the 21st century.", true, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, "Tech Revolution", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("47e73383-0b1d-4f95-8282-8aee18cc6370"), new Guid("706da7f1-ddc2-4811-9b6b-81a2b9b76667"), "https://example.com/dragons_call.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A magical story of dragons and heroes.", true, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "The Dragon's Call", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("65b9afd2-ad36-4f69-a998-fa7e69e144d4"), new Guid("2f6edd3c-4872-46eb-906f-394f09a42360"), "https://example.com/space_odyssey.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A thrilling adventure across galaxies.", true, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, "Space Odyssey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ef2d97ca-f964-469c-9a45-24c757825fea"), new Guid("80cd7239-2286-4b89-b2ae-6df0a28615f2"), "https://example.com/life_of_a_visionary.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A biography of one of the most inspiring innovators of the century.", true, new DateTime(2021, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, "Life of a Visionary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f39d75f5-9564-453a-9fea-bb192e8422c0"), new Guid("a1dd6d4a-4f98-44ab-937a-aa2156e4c728"), "https://example.com/lost_city.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A mystery novel set in an ancient city filled with secrets.", true, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 70, "The Mystery of the Lost City", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "BookId", "CategoryId" },
                values: new object[,]
                {
                    { new Guid("460fb0d4-289c-43d9-8055-1fbcf992dcb3"), new Guid("768ddc89-8250-4ec1-a693-42dacebf27ab") },
                    { new Guid("460fb0d4-289c-43d9-8055-1fbcf992dcb3"), new Guid("a0d2810e-5d51-4fa4-b401-7353a03e582d") },
                    { new Guid("47e73383-0b1d-4f95-8282-8aee18cc6370"), new Guid("694f18c2-de17-4e1e-9702-d146183803ed") },
                    { new Guid("47e73383-0b1d-4f95-8282-8aee18cc6370"), new Guid("768ddc89-8250-4ec1-a693-42dacebf27ab") },
                    { new Guid("65b9afd2-ad36-4f69-a998-fa7e69e144d4"), new Guid("694f18c2-de17-4e1e-9702-d146183803ed") },
                    { new Guid("65b9afd2-ad36-4f69-a998-fa7e69e144d4"), new Guid("a0d2810e-5d51-4fa4-b401-7353a03e582d") },
                    { new Guid("ef2d97ca-f964-469c-9a45-24c757825fea"), new Guid("05b0ecd6-dc9d-4901-87db-08d23fcd940f") },
                    { new Guid("ef2d97ca-f964-469c-9a45-24c757825fea"), new Guid("694f18c2-de17-4e1e-9702-d146183803ed") },
                    { new Guid("f39d75f5-9564-453a-9fea-bb192e8422c0"), new Guid("05b0ecd6-dc9d-4901-87db-08d23fcd940f") },
                    { new Guid("f39d75f5-9564-453a-9fea-bb192e8422c0"), new Guid("768ddc89-8250-4ec1-a693-42dacebf27ab") }
                });

            migrationBuilder.InsertData(
                table: "BookPrices",
                columns: new[] { "Id", "BookId", "CreatedAt", "IsActive", "Price", "UpdatedAt", "ValideFrom" },
                values: new object[,]
                {
                    { new Guid("15201511-cf0a-49c3-b1ed-e5d77a738884"), new Guid("460fb0d4-289c-43d9-8055-1fbcf992dcb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 70m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3171cc65-f0fe-49b9-acc6-09903c4f813f"), new Guid("65b9afd2-ad36-4f69-a998-fa7e69e144d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 13m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3c065bad-f770-4b7e-b43a-7c3f430be3ef"), new Guid("460fb0d4-289c-43d9-8055-1fbcf992dcb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 65m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("404a0921-1dc5-4e59-8401-85339104b514"), new Guid("47e73383-0b1d-4f95-8282-8aee18cc6370"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 12m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4d7f7254-7c21-40d1-a1b4-4ecd34bfc01b"), new Guid("65b9afd2-ad36-4f69-a998-fa7e69e144d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1.3m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5c67cacb-b117-4234-b01a-2019393ca824"), new Guid("47e73383-0b1d-4f95-8282-8aee18cc6370"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 20m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("850f45ee-21c8-4a3e-9eb4-745063787877"), new Guid("ef2d97ca-f964-469c-9a45-24c757825fea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 40m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a0ad39a9-7e2b-4d66-8e1d-663477235065"), new Guid("460fb0d4-289c-43d9-8055-1fbcf992dcb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 62m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d03e0c70-f57d-47be-851e-31bd6003b419"), new Guid("ef2d97ca-f964-469c-9a45-24c757825fea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 90m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f4ca88d8-2bae-44d2-bd64-02ecf91a12ec"), new Guid("f39d75f5-9564-453a-9fea-bb192e8422c0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 60m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fba213c2-199f-413d-9dd7-be44d2b90aca"), new Guid("ef2d97ca-f964-469c-9a45-24c757825fea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 62m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name",
                table: "Authors",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CategoryId",
                table: "BookCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPrices_BookId",
                table: "BookPrices",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title_Id",
                table: "Books",
                columns: new[] { "Title", "Id" },
                unique: true,
                filter: "[Title] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "BookPrices");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
