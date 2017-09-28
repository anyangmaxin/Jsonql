using System;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Sample.AspNetCore
{
    public class UserAccountRecord : Dao
    {
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public decimal Coins { get; set; }

        public decimal Points { get; set; }

        [MaxLength(50)]
        public string Remark { get; set; }

        [Required]
        public User User { get; set; }
    }
}
