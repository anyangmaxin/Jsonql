using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Demo.AspNet
{
    public class User : Dao
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        public UserProfile Profile { get; set; }

        public UserAccount Account { get; set; }

        public ICollection<UserAccountRecord> AccountRecords { get; set; } = new List<UserAccountRecord>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
