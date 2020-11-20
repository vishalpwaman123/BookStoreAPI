using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class UpdateBookModel
    {

        public int BookId { get; set; }

        
        public string BookName { get; set; }

        
        public string AuthorName { get; set; }

        
        public string Description { get; set; }

        
        public int Price { get; set; }

        
        public int Pages { get; set; }

        
        public int Quantity { get; set; }

        
        public IFormFile Image { get; set; }

    }
}
