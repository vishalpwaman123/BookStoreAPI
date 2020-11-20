using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class WishListModel
    {
        [Required(ErrorMessage = "BookId is Required")]
        [RegularExpression(@"^[0-9]+$")]
        public int BookId { get; set; }

    }
}
