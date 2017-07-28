using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace core.DbModels
{
    public class Auth
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public Guid AuthKey { get; set; }
    }
}
