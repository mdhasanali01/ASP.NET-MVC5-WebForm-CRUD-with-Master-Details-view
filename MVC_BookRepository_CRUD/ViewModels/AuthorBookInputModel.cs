using MVC_BookRepository_CRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_BookRepository_CRUD.ViewModels
{
    public class AuthorBookInputModel
    {
        public int BookId { get; set; }
        [Required, StringLength(50), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Publish Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Required, Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        public bool Available { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [Required, Display(Name = "Publisher Id")]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public List<AuthorViewModel> Authors { get; set; } = new List<AuthorViewModel>();

    }
}