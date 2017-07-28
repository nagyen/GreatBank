using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace core.DbModels
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
