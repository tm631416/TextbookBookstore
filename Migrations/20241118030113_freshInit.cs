using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextbookBookstore.Migrations
{
    public partial class freshInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookCover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBookStatuses",
                columns: table => new
                {
                    UserBookStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookStatuses", x => x.UserBookStatusId);
                    table.ForeignKey(
                        name: "FK_UserBookStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookStatuses_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName" },
                values: new object[,]
                {
                    { 1, "C#.Net" },
                    { 2, "ASP.NET Using C#" },
                    { 3, "Java" },
                    { 4, "JavaScript & jQuery" },
                    { 5, "Data Analysis With Python" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "LanguageName" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "ASP.Net" },
                    { 3, "Java" },
                    { 4, "JavaScript" },
                    { 5, "Python" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "BookCover", "BookDescription", "BookStatus", "ClassId", "LanguageId", "Price", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, "Joel Murach", "images/MurachCSharpBook.jpg", "The 8th Edition of Murach's C# does a better job than ever of teaching the C# programming language. Each section features clear examples and easy-to-understand explanations that walk you through crucial skills, best practices, and helpful tips.\r\n\r\nUsing this book as your guide, you'll get off to a fast start by taking advantage of the best features of Visual Studio, C#, and the .NET classes to develop Windows Forms apps. Because of its self-paced approach, this book works equally well whether you're new to programming or an experienced developer.\r\n\r\nAfter presenting some essential C# skills, this book shows how to write object-oriented code the way it's done in the real world. It also shows you how to work with a database using EF (Entity Framework) or ADO.NET. When you're done, you'll be able to develop 3-tier, object-oriented, Windows Forms apps that work with a database. More importantly, you'll have a solid set of C# skills that you can apply to any C# app whether it's for the desktop, the web, or mobile devices.\r\n\r\nEvery Murach book guarantees high quality. The complete apps show how each feature works in context. The exercises at the end of each chapter let you practice your new skills and gain valuable hands-on experience. And the distinctive \"paired-pages\" format is ideal for learning and reference.", "Not Started", 1, 1, 45.57, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Murach's C# 8th Edition" },
                    { 2, "Mary Delamater", "images/MurachASPNetBook.jpg", "This 2nd Edition of Murach’s ASP.NET Core MVC does a better job than ever of delivering the skills you need to develop websites using the MVC (Model-View-Controller) pattern with ASP.NET Core. If you know the basics of C#, you’ll quickly learn to code the way today’s top web professionals do. Each section features clear, beginner-friendly examples and easy-to-understand explanations that walk you through crucial skills, best practices, and helpful tips.\r\n\r\n“I’m a first-time customer who has recently purchased your ASP.NET Core MVC book, and I have to say I’m greatly impressed. [It] was actually fun from start to finish (and I've read many, many programming books before).” - Shannon Fairchild, Senior Software Developer, Kingston, Ontario, Canada\r\n\r\nSection 1 (just 5 chapters) shows how to develop responsive web apps that follow the MVC pattern so they’ll be easy to maintain as they grow and change. Then, it shows how to test and debug these apps using the debugging tools provided by Visual Studio and your browser.\r\n\r\nSection 2 builds out that set of skills to create more complex controllers, work with Razor views, handle cookies and sessions, work with model binding, validate data, and use EF Core to work with databases.\r\n\r\nFinally, section 3 presents additional skills that you can learn when you need them. Automate testing by using dependency injection and unit tests. Reduce code duplication by creating custom tag helpers and view components. Control user access to a site with ASP.NET Core Identity. Deploy a site to the cloud with Azure. And use Visual Studio Code, an increasingly popular alternative to the Visual Studio IDE.\r\n\r\nEvery Murach book guarantees high quality. The complete apps show how each feature works in context. The exercises at the end of each chapter let you practice your new skills and gain valuable hands-on experience. And the distinctive “paired-pages” format is ideal for learning and reference.", "Not Started", 2, 2, 43.189999999999998, new DateTime(2022, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Murach's ASP.NET Core MVC 2nd Edition" },
                    { 3, "Joel Murach", "images/MurachJavaBook.jpg", "If you want to learn Java programming but don’t know where to start, this is the right book for you. From the first page, our unique self-paced approach will help you build competence and confidence in your programming skills, even if you’re completely new to programming.\r\n\r\nBut this isn’t just a book for beginners! Our self-paced approach also works for experienced programmers, helping you learn Java faster and better than you’ve ever learned a language before. By the time you’re through, you will have mastered all of the Java skills that are needed on the job, including the skills for developing object-oriented applications that use a graphical user interface (GUI) and a database.\r\n\r\nTo make this possible, section 1 of this book presents a 9-chapter course that gets anyone off to a great start building object-oriented applications in Java. Then, the next 3 sections build on that base by presenting more on object-oriented programming, the essentials for developing GUIs, and additional skills that every professional Java programmer should have, including how to work with a database.", "Not Started", 3, 3, 52.990000000000002, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Murach's Java Programming 6th Edition" },
                    { 4, "Zak Ruvalcaba", "images/MurachJavaScriptBook.jpg", "If you’re developing websites, you have to know JavaScript. There’s no way around it today.\r\n\r\nAnd this latest edition of Murach’s popular book teaches you how to code modern JavaScript that conforms to the ECMAScript standards, the way the pros do. At the same time, it teaches you how to use jQuery, the classic JavaScript library, to handle the DOM scripting that gives JavaScript so much of its power. And it works no matter whether you’re a web designer who’s coming from a background in HTML and CSS or a server-side programmer who’s coded in languages like PHP, C#, Java, and Python.\r\n\r\nHow is all that possible? Section 1 presents a 7-chapter course on JavaScript that will get anyone off to a great start, with a special focus on the skills that you need to get the most from jQuery. Then, section 2 presents the jQuery skills that you’re likely to use, including how to create slide shows, image swaps, carousels, and accordions...how to validate the data in forms...and how to use plugins and widgets.\r\n\r\nAt that point, you’ll have a solid set of JavaScript and jQuery skills. Then, section 3 lets you expand your skill set as you learn how to work with date and time objects, browser objects, web storage, arrays, your own objects, regular expressions, and more. Finally, section 4 takes your skills to a new level as you learn how to use ECMAScript modules, work with Promises and Ajax, and get started using Node.js.\r\n\r\nComplete coding examples, practice exercises, and Murach’s distinctive “paired-pages” format (each topic is presented in a 2-page spread with text and illustrations) all combine to let you tailor the pace and content to your personal learning style. Get your copy today, and see for yourself!", "Not Started", 4, 4, 40.82, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Murach's JavaScript and jQuery 4th Edition" },
                    { 5, "Michael Urban", "images/MurachPythonBook.jpg", "If you want to learn how to program but don’t know where to start, this is the right book and the right language for you. From the first page, our self-paced approach will help you build competence and confidence in your programming skills. And Python is the best language ever for learning how to program because of its simplicity and breadth…two features that are hard to find in a single language.\r\n\r\nBut this isn’t just a book for beginners! Our self-paced approach also works for experienced programmers, helping you learn Python faster and better than you’ve ever learned a language before. By the time you’re through, you will have mastered the key Python skills that are needed on the job, including those for object-oriented, database, and GUI programming.\r\n\r\nTo make all of this possible, section 1 presents an 8-chapter course that will get anyone off to a great start with Python. Section 2 builds on that base by presenting the other essential skills that every Python programmer should have. Section 3 shows you how to develop object-oriented programs, a critical skillset in today’s world. And section 4 shows you how to apply all of the skills that you’ve already learned as you build database and GUI programs for the real world.", "Not Started", 5, 5, 46.990000000000002, new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Murach's Python Programming 2nd Edition" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ClassId",
                table: "Books",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageId",
                table: "Books",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BookId",
                table: "CartItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BookId",
                table: "OrderDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookStatuses_BookId",
                table: "UserBookStatuses",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookStatuses_UserId",
                table: "UserBookStatuses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "UserBookStatuses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
