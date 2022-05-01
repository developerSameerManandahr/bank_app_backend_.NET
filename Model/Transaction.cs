using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace worksheet2.Model
{
    public class Transaction
    {
        [Key] [ForeignKey("User")] public string UserTransactionId { get; set; }

        public virtual User User { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}