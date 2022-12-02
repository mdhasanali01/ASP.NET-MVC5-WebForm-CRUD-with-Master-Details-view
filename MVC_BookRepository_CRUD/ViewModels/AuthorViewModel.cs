using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BookRepository_CRUD.ViewModels
{
    public class AuthorViewModel
    {
        [Required]
        public int AuthorId { get; set; }
        
        public string Address { get; set; }
    }
}