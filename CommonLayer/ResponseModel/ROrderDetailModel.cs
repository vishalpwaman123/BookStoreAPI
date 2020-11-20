using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class ROrderDetailModel
    {

       public int OrderID;

        public int UserID;

        public int CartID;

        public int AddressID;

        public string IsActive;
      
        public string IsPlaced;

        public int Quantity;

        public int TotalPrice;

        public string CreatedDate;

    }
}
