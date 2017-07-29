using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace core.DbModels
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public decimal PrevBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        // transaction type enum
        public enum TransactionType
        {
            Deposit = 0,
            Withdrawal = 1
        }

        // helpers
        public TransactionType GetTransactionType()
        {
            return (TransactionType)Type;
        }
    }
}
