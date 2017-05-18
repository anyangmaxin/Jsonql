using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Sample.AspNetCore
{
    public class User : Dao
    {
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        public UserProfile Profile { get; set; }

        public UserAccount Account { get; set; }

        public ICollection<UserAccountRecord> AccountRecords { get; set; } = new List<UserAccountRecord>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
