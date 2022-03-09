﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace worksheet2.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [StringLength(20)] public string UserName { get; set; }

        [StringLength(50)] public string Password { get; set; }

        [StringLength(4)] public int Pin { get; set; }

        [StringLength(10)] public string AccountNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        public  virtual UserDetails UserDetails { get; set; }
        public  virtual AccountDetails AccountDetails { get; set; }
        public  virtual ICollection<Transaction> Transactions { get; set; }
    }
}