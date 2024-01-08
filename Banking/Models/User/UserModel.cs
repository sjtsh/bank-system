using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Banking.Models
{
    public partial class UserModel : IdentityUser
    {
        #region Constructor
        /// <summary>
        /// Signup for normal user
        /// </summary>
        public UserModel(string phoneNumber, string firstName, string? middleName, string lastName, string email, string password, int bankId) {
            SetPhoneNumber(phoneNumber);
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            SetEmail(email);
            Password = password;
            BankId = bankId;
        }

        /// <summary>
        /// Sign up for Admin User
        /// </summary>
        public UserModel(string phoneNumber, string firstName, string lastName, string password)
        {
            SetPhoneNumber(phoneNumber);
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            IsAdmin = true;
        }

        /// <summary>
        /// constructor without the transations
        /// </summary>
        public UserModel()
        {
        }
        #endregion

        #region Fields
        private string _firstName = string.Empty;
        private string? _middleName;
        private string _lastName = string.Empty;
        private string _password = string.Empty;

        #endregion
        #region Properties
        #endregion


        #region Properties
        public string GetPhoneNumber()
        { return PhoneNumber!; }
        #endregion


        #region Properties
        public void SetPhoneNumber(string value)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Phone number can't be empty.");
            }
            if (value.Length != 10)
            {
                throw new ArgumentException("Phone number must be of length ten.");
            }
            if (int.TryParse(value, out _))
            {
                throw new ArgumentException("Phone number is invalid.");
            }
            PhoneNumber = value;
            UserName = value;
        }

        public string? GetEmail()
        { return Email; }


        public void SetEmail(string? value)
        {
            if (value == null) return;
            value = value.Trim();
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email can't be empty.");
            }
            if (!EmailRegex().Match(value).Success)
            {
                throw new ArgumentException("Email doesn't have a correct format.");
            }
            Email = value;
        } 

        public string FullName
        {
            get
            {
                string fullName = FirstName + " ";
                if (MiddleName != null) fullName += MiddleName + " ";
                return fullName + LastName;
            }
        }

        public string FirstName 
        { 
            get { return _firstName; } set
            {
                value = value.Trim();
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name can't be empty.");
                }
                if (value.Length < 3)
                {
                    throw new ArgumentException("First name must be of length more than three.");
                }
                if (value.Length >= 14)
                {
                    throw new ArgumentException("First name must be of length less than fourteen.");
                }
                _firstName = value;
            } 
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                value = value.Trim();
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name can't be empty.");
                }
                if (value.Length < 2)
                {
                    throw new ArgumentException("Last name must be of length more than two.");
                }
                if (value.Length >= 14)
                {
                    throw new ArgumentException("Last name must be of length less than fourteen.");
                }
                _lastName = value;
            }
        }

        //We dont need password in the user model as it will be managed by the usermanager
        [NotMapped]
        public string Password
        {
            get { return _password; }
            set
            {
                value = value.Trim();
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password can't be empty.");
                }
                if(!UpperLowercaseCharacterRegex().Match(value).Success)
                {
                    throw new ArgumentException("Password must have atleast 1 uppercase, 1 lowercase character");
                }
                if (!NonAlphanumericCharacterRegex().Match(value).Success)
                {
                    throw new ArgumentException("Password must have atleast 1 non alphanumeric character");
                }
                if (!MinCharacterRegex().Match(value).Success)
                {
                    throw new ArgumentException("Password must be of atleast 8 characters with atleast 1 letter and 1 number");
                }
                _password = value;
            }
        }

        /// Non required fields
        public string? MiddleName
        {
            get { return _middleName; }
            set
            {
                if (value == null) return;
                value = value.Trim();
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Middle name can't be empty.");
                }
                if (value.Length < 2)
                {
                    throw new ArgumentException("Middle name must be of length more than two.");
                }
                if (value.Length >= 14)
                {
                    throw new ArgumentException("Middle name must be of length less than fourteen.");
                }
                _middleName = value;
            }
        }

        public double Balance { get; set; } = 0;
        public bool IsAdmin { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        #endregion


        #region Foreign 
        public int? BankId { get; set; }
        public virtual BankModel? Bank { get; set; }

        [ForeignKey("RecieverId")]
        public virtual ICollection<UserTransactionModel>? RecievedTransactions { get; set; }

        [ForeignKey("SenderId")]
        public virtual ICollection<UserTransactionModel>? SentTransactions { get; set; }
        #endregion


        #region Generated Fields
        public int? AccountNumber { get; set; }

        [Display(Name = "LastActivity")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastActivity { get; set; }
        #endregion

        #region Regex Helpers 
        // [NotMapped]
        [GeneratedRegex(@"^([a-zA-Z0-9éèà]+[a-zA-Z0-9.-éèàïëöüäîôûêâù]*)@([a-zA-Z]+)[.]([a-z]+)([.][a-z]+)*$")]
        private static partial Regex EmailRegex();

        /// At least 8 characters with one letter and one number
        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z]).{8,}$")]
        private static partial Regex MinCharacterRegex();

        /// At least a characters with one uppercase
        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z]).+$")]
        private static partial Regex UpperLowercaseCharacterRegex();

        /// At least a characters with one non alphanumeric character
        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).+$")]
        private static partial Regex NonAlphanumericCharacterRegex();

        

        #endregion
    }
}
 