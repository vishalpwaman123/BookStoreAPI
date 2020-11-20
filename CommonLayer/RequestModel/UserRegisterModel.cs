using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class UserRegisterModel
    {

        [Required(ErrorMessage = "Firstname is Required")]
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is Required")]
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "EmailId is Required")]
        [RegularExpression("^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$", ErrorMessage = "EmailId is not valid")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Please Enter Minimum 6 Characters ")]
        public string Password { get; set; }

        /*public string Locality { get; set; }

        public string City { get; set; }

        public string State { get; set; }*/

        [Required(ErrorMessage = "Phone Number is Required")]
        [RegularExpression("([1-9]{1}[0-9]{9})$")]
        public string PhoneNumber { get; set; }

        /*public string PinCode { get; set; }

        public string LandMark { get; set; }*/
    }
}
