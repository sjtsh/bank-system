using System.ComponentModel.DataAnnotations;

namespace Banking_B.Models
{
    public class UserModel
    {
        public int? Id { get; set; } 
        public string AccountNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }


        [Display(Name = "LastActivity")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastActivity { get; set; }
        public BankModel Bank { get; set; }

        public string FullName { get {  return FirstName + " " + MiddleName + " " + LastName; } }
    }
}
