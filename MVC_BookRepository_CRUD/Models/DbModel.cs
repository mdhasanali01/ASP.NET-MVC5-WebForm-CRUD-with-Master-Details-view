using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_BookRepository_CRUD.Models
{
    
    public class Author
    {
        public int AuthorId { get; set; }
        [Required, StringLength(50), Display(Name ="Author Name")]
        public string AuthorName { get; set; }
         [Required , StringLength(50), Display(Name ="Address")]
        public string Address { get; set; }
        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    }
    public class Topic
    {
        public int TopicId { get; set; }
        [Required, StringLength(30),Display(Name ="Topic Name")]
        public string TopicName { get; set; }       
    }
    public class Publisher
    {
        public  int PublisherId { get; set; }
        [Required, StringLength(50), Display(Name ="Publisher Name")]
        public string PublisherName { get; set; }
        [Required, StringLength(50), Display(Name ="Address")]
        public string Address { get; set; }
        [Required, StringLength(50), EmailAddress()]
        public string Email { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
    public class Book
    {
        public int BookId { get; set; }
        [Required,StringLength(50), Display(Name ="Book Name")]
        public string BookName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Publish Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Required, Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        public bool Available { get; set; }
        [Required,StringLength(200)]
        public string Picture { get; set; }
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
    }
    public class AuthorBook
    {
        [Key, Column(Order = 0), ForeignKey("Author")]
        public int AuthorId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Book")]
        public int BookId { get; set; }
        public  Author Author { get; set; }
        public  Book Book { get; set; }
    }
    public class BookRepositoryDbContext : DbContext
    {
        public BookRepositoryDbContext()
        {
            Database.SetInitializer(new BookRepositoryDbInitializer());
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Publisher> Publishers { get; set; } 
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
    }
    public class BookRepositoryDbInitializer : DropCreateDatabaseIfModelChanges<BookRepositoryDbContext>
    {
        protected override void Seed(BookRepositoryDbContext context)
        {
            Author a = new Author { AuthorName = "Hasan", Address = "Dhaka" };
            Topic t = new Topic { TopicName = "Geograpy" };
            Publisher p = new Publisher { PublisherName = "Punjeri publication", Address = "Dhaka", Email = "Pan@gmail.com" };
            Book b = new Book { BookName = "Programing With C#", PublishDate = new DateTime(2010, 10, 10), Available = true, Price = 500.00M, Picture = "1.jpg", Publisher = p };
            p.Books.Add(b);
            context.Authors.Add(a);
            context.Topics.Add(t);
            context.Publishers.Add(p);
            context.AuthorBooks.Add(new AuthorBook { Author=a, Book=b });
            context.SaveChanges();
        }
    }
}