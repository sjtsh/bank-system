﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public class UserTransactionModel
    {
        #region Constructor
        /// <summary>
        /// Create transaction model
        /// </summary>
        public UserTransactionModel(string? remark, string recieverId, string senderId, double amount)
        {
            Remark = remark;
            RecieverId = recieverId;
            SenderId = senderId;
            Amount = amount;
            CreatedAt = DateTime.Now;
        }
        public UserTransactionModel(string recieverId, double amount)
        {
            RecieverId = recieverId;
            Amount = amount;
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
            get
            {
                if (Sender?.Id == null) return "Sign In Bonus";
                return _remark; 
            }
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
        public string RecieverId { get; set; }
        public virtual UserModel? Reciever { get; set; }
        public string? SenderId { get; set; }
        public virtual UserModel? Sender { get; set; }
        #endregion

        #region Generated Fields
        [Display(Name = "CreatedAt")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        #endregion
        
        public string? GetOtherUserName(string? userId)
        {
            if (userId == null) return null;
            if(Sender?.Id == userId)return Reciever?.FullName;
            return Sender?.FullName;
        }
        public string? GetOtherBankName(string? userId)
        {
            if (userId == null) return null;
            if (Sender?.Id == userId)return Reciever?.Bank?.Name;
            return Sender?.Bank?.Name;
        }
        public string? GetStatus(string? userId)
        {
            if (userId == null) return null; 
            if (Sender?.Id == userId) return "Debit";
            return "Credit";
        }
    }
}
