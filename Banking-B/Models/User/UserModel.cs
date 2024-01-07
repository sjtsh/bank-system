using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Banking_B.Models
{
    public partial class UserModel
    {
        #region Constructor
        /// <summary>
        /// Signup for normal user
        /// </summary>
        public UserModel(string phoneNumber, string firstName, string? middleName, string lastName, string email, string password) {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Sign up for Admin User
        /// </summary>
        public UserModel(string phoneNumber, string firstName, string lastName, string password)
        {
            _phoneNumber = phoneNumber;
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
        private string _phoneNumber = string.Empty;
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string? _middleName;
        private string _lastName = string.Empty;
        private string _password = string.Empty;
        #endregion


        #region Properties
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
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
                _phoneNumber = value;
            }
        }
        public string? Email
        {
            get
            { return _email; }
            set
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
                _email = value;
            }
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
        public int BankId { get; set; }
        public virtual BankModel? Bank { get; set; }

        [ForeignKey("RecieverId")]
        public virtual ICollection<UserTransactionModel>? RecievedTransactions { get; set; }

        [ForeignKey("SenderId")]
        public virtual ICollection<UserTransactionModel>? SentTransactions { get; set; }
        #endregion


        #region Generated Fields
        public int? AccountNumber { get; set; }

        [Key]
        public int? Id { get; set; }

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
        [GeneratedRegex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")]
        private static partial Regex MinCharacterRegex();
        #endregion
    }
}
 