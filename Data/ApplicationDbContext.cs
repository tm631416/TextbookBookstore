using TextbookBookstore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TextbookBookstore.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TextbookBookstore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<UserBookStatus> UserBookStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Explicitly configure the primary key for IdentityUserLogin
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(login => new { login.LoginProvider, login.ProviderKey });
            });

            builder.Entity<Language>().HasData(
                new Language { LanguageId = 1, LanguageName = "C#" },
                new Language { LanguageId = 2, LanguageName = "ASP.Net" },
                new Language { LanguageId = 3, LanguageName = "Java" },
                new Language { LanguageId = 4, LanguageName = "JavaScript" },
                new Language { LanguageId = 5, LanguageName = "Python" }
            );

            builder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Murach's C# 8th Edition",
                    Author = "Joel Murach",
                    Price = 45.57,
                    PublishedDate = new DateTime(2023, 04, 01),
                    BookStatus = "Not Started",
                    BookCover = "MurachCSharpBook.jpg",
                    BookDescription = "The 8th Edition of Murach's C# does a better job than ever of teaching the C# programming language. Each section features clear examples and easy-to-understand explanations that walk you through crucial skills, best practices, and helpful tips.\r\n\r\nUsing this book as your guide, you'll get off to a fast start by taking advantage of the best features of Visual Studio, C#, and the .NET classes to develop Windows Forms apps. Because of its self-paced approach, this book works equally well whether you're new to programming or an experienced developer.\r\n\r\nAfter presenting some essential C# skills, this book shows how to write object-oriented code the way it's done in the real world. It also shows you how to work with a database using EF (Entity Framework) or ADO.NET. When you're done, you'll be able to develop 3-tier, object-oriented, Windows Forms apps that work with a database. More importantly, you'll have a solid set of C# skills that you can apply to any C# app whether it's for the desktop, the web, or mobile devices.\r\n\r\nEvery Murach book guarantees high quality. The complete apps show how each feature works in context. The exercises at the end of each chapter let you practice your new skills and gain valuable hands-on experience. And the distinctive \"paired-pages\" format is ideal for learning and reference.",
                    LanguageId = 1
                },
                new Book
                {
                    BookId = 2,
                    Title = "Murach's ASP.NET Core MVC 2nd Edition",
                    Author = "Mary Delamater",
                    Price = 43.19,
                    PublishedDate = new DateTime(2022, 11, 22),
                    BookStatus = "Not Started",
                    BookCover = "MurachASPNetBook.jpg",
                    BookDescription = "This 2nd Edition of Murach’s ASP.NET Core MVC does a better job than ever of delivering the skills you need to develop websites using the MVC (Model-View-Controller) pattern with ASP.NET Core. If you know the basics of C#, you’ll quickly learn to code the way today’s top web professionals do. Each section features clear, beginner-friendly examples and easy-to-understand explanations that walk you through crucial skills, best practices, and helpful tips.\r\n\r\n“I’m a first-time customer who has recently purchased your ASP.NET Core MVC book, and I have to say I’m greatly impressed. [It] was actually fun from start to finish (and I've read many, many programming books before).” - Shannon Fairchild, Senior Software Developer, Kingston, Ontario, Canada\r\n\r\nSection 1 (just 5 chapters) shows how to develop responsive web apps that follow the MVC pattern so they’ll be easy to maintain as they grow and change. Then, it shows how to test and debug these apps using the debugging tools provided by Visual Studio and your browser.\r\n\r\nSection 2 builds out that set of skills to create more complex controllers, work with Razor views, handle cookies and sessions, work with model binding, validate data, and use EF Core to work with databases.\r\n\r\nFinally, section 3 presents additional skills that you can learn when you need them. Automate testing by using dependency injection and unit tests. Reduce code duplication by creating custom tag helpers and view components. Control user access to a site with ASP.NET Core Identity. Deploy a site to the cloud with Azure. And use Visual Studio Code, an increasingly popular alternative to the Visual Studio IDE.\r\n\r\nEvery Murach book guarantees high quality. The complete apps show how each feature works in context. The exercises at the end of each chapter let you practice your new skills and gain valuable hands-on experience. And the distinctive “paired-pages” format is ideal for learning and reference.",
                    LanguageId = 2
                },
                new Book
                {
                    BookId = 3,
                    Title = "Murach's Java Programming 6th Edition",
                    Author = "Joel Murach",
                    Price = 52.99,
                    PublishedDate = new DateTime(2022, 02, 01),
                    BookStatus = "Not Started",
                    BookCover = "MurachJavaBook.jpg",
                    BookDescription = "If you want to learn Java programming but don’t know where to start, this is the right book for you. From the first page, our unique self-paced approach will help you build competence and confidence in your programming skills, even if you’re completely new to programming.\r\n\r\nBut this isn’t just a book for beginners! Our self-paced approach also works for experienced programmers, helping you learn Java faster and better than you’ve ever learned a language before. By the time you’re through, you will have mastered all of the Java skills that are needed on the job, including the skills for developing object-oriented applications that use a graphical user interface (GUI) and a database.\r\n\r\nTo make this possible, section 1 of this book presents a 9-chapter course that gets anyone off to a great start building object-oriented applications in Java. Then, the next 3 sections build on that base by presenting more on object-oriented programming, the essentials for developing GUIs, and additional skills that every professional Java programmer should have, including how to work with a database.",
                    LanguageId = 3
                },
                new Book
                {
                    BookId = 4,
                    Title = "Murach's JavaScript and jQuery 4th Edition",
                    Author = "Zak Ruvalcaba",
                    Price = 40.82,
                    PublishedDate = new DateTime(2020, 09, 01),
                    BookStatus = "Not Started",
                    BookCover = "MurachJavaScriptBook.jpg",
                    BookDescription = "If you’re developing websites, you have to know JavaScript. There’s no way around it today.\r\n\r\nAnd this latest edition of Murach’s popular book teaches you how to code modern JavaScript that conforms to the ECMAScript standards, the way the pros do. At the same time, it teaches you how to use jQuery, the classic JavaScript library, to handle the DOM scripting that gives JavaScript so much of its power. And it works no matter whether you’re a web designer who’s coming from a background in HTML and CSS or a server-side programmer who’s coded in languages like PHP, C#, Java, and Python.\r\n\r\nHow is all that possible? Section 1 presents a 7-chapter course on JavaScript that will get anyone off to a great start, with a special focus on the skills that you need to get the most from jQuery. Then, section 2 presents the jQuery skills that you’re likely to use, including how to create slide shows, image swaps, carousels, and accordions...how to validate the data in forms...and how to use plugins and widgets.\r\n\r\nAt that point, you’ll have a solid set of JavaScript and jQuery skills. Then, section 3 lets you expand your skill set as you learn how to work with date and time objects, browser objects, web storage, arrays, your own objects, regular expressions, and more. Finally, section 4 takes your skills to a new level as you learn how to use ECMAScript modules, work with Promises and Ajax, and get started using Node.js.\r\n\r\nComplete coding examples, practice exercises, and Murach’s distinctive “paired-pages” format (each topic is presented in a 2-page spread with text and illustrations) all combine to let you tailor the pace and content to your personal learning style. Get your copy today, and see for yourself!",
                    LanguageId = 4
                },
                new Book
                {
                    BookId = 5,
                    Title = "Murach's Python Programming 2nd Edition",
                    Author = "Michael Urban",
                    Price = 46.99,
                    PublishedDate = new DateTime(2021, 04, 01),
                    BookStatus = "Not Started",
                    BookCover = "MurachPythonBook.jpg",
                    BookDescription = "If you want to learn how to program but don’t know where to start, this is the right book and the right language for you. From the first page, our self-paced approach will help you build competence and confidence in your programming skills. And Python is the best language ever for learning how to program because of its simplicity and breadth…two features that are hard to find in a single language.\r\n\r\nBut this isn’t just a book for beginners! Our self-paced approach also works for experienced programmers, helping you learn Python faster and better than you’ve ever learned a language before. By the time you’re through, you will have mastered the key Python skills that are needed on the job, including those for object-oriented, database, and GUI programming.\r\n\r\nTo make all of this possible, section 1 presents an 8-chapter course that will get anyone off to a great start with Python. Section 2 builds on that base by presenting the other essential skills that every Python programmer should have. Section 3 shows you how to develop object-oriented programs, a critical skillset in today’s world. And section 4 shows you how to apply all of the skills that you’ve already learned as you build database and GUI programs for the real world.",
                    LanguageId = 5
                }
                );
        }
    }
}