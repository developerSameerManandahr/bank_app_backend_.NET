﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace worksheet2.Model
{
    public class AccountDetails
    {
        [Key] 
        public string UserAccountDetailsId { get; set; }

        public  User User { get; set; }

        public long Balance { get; set; }
        public AccountType AccountType { get; set; }
        public string Currency { get; set; }
    }

    public enum AccountType
    {
        CURRENT,
        SAVING,
        PREMIUM
    }
}