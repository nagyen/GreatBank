using System;
namespace core
{
    public class TransactionModels
    {
        public class TransactionFeedback
        {
            public DbModels.Transaction.TransactionType TransactionType { get; set; }
            public string Errors { get; set; }
            public bool Success { get; set; }
        }
    }
}
