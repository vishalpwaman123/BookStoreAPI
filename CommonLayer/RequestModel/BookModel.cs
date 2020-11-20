using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class BookModel
    {

        [Required(ErrorMessage = "Book Name Required")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Author Name Required")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Book Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price Of Book Required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Pages Required")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Book Quantity Required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Image Required")]
        public IFormFile Image { get; set; }

    }
}
