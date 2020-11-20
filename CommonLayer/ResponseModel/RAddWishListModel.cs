using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class RAddWishListModel
    {

        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string IsMoved { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
