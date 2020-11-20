using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class RUserRegisterModel
    {

        public int UserId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string UserRole { get; set; }

        public string CreatedDate { get; set; }

    }
}
