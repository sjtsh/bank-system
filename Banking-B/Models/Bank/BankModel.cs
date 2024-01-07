using System.ComponentModel.DataAnnotations;

namespace Banking_B.Models
{
    public class BankModel
    {

        #region Constructor
        /// <summary>
        /// Bank creation model
        /// </summary>
        public BankModel(string name)
        {
            Name=name;
        }
        #endregion

        #region Fields
        private string _name = string.Empty;
        #endregion

        #region Properties
        [Key]
        public int? Id { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                value = value.Trim();
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name name can't be empty.");
                }
                if (value.Length < 5)
                {
                    throw new ArgumentException("Bank name must be of length more than five.");
                }
                if (value.Length >= 40)
                {
                    throw new ArgumentException("Bank name must be of length less than fourty.");
                }
                _name = value;
            }
        }

        public double TotalBalance { get; set; } = 0;
        public double TotalWithdrawl { get; set; } = 0;
        public double TotalDeposit { get; set; } = 0;
        #endregion

        #region Foreign 
        public virtual ICollection<UserModel>? Users { get; set; }
        #endregion
    }
}
