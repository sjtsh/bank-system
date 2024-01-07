using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_B.Models
{
    public class UserTransactionModel
    {
        #region Constructor
        /// <summary>
        /// Create transaction model
        /// </summary>
        public UserTransactionModel(string remark, int recieverId, int senderId)
        {
            Remark = remark;
            RecieverId = recieverId;
            SenderId = senderId;
            CreatedAt = DateTime.Now;
        }
        #endregion

        #region Fields
        private double _amount = 0;
        private string? _remark;
        #endregion

        #region Properties
        [Key]
        public int? Id { get; set; }
        public string? Remark
        {
            get { return _remark; }
            set
            {
                if (value == null) return;
                value = value.Trim();
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Remarks can't be empty.");
                }
                if (value.Length >= 40)
                {
                    throw new ArgumentException("Remarks must be of length less than fourty.");
                }
                _remark = value;
            }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {;
                if (value <= 0)
                {
                    throw new ArgumentException("Amount must be greater than zero.");
                }
                _amount = value;
            }
        }

        public double ClosingBalance { get; set; } = 0;
        public bool IsInterBank { get; set; } = false;
        #endregion

        #region Foreign 
        public int RecieverId { get; set; }
        public virtual UserModel? Reciever { get; set; }
        public int SenderId { get; set; }
        public virtual UserModel? Sender { get; set; }
        #endregion

        #region Generated Fields
        [Display(Name = "CreatedAt")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        #endregion
    }
}
