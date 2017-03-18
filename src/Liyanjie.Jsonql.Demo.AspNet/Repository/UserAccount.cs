﻿using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Demo.AspNet
{
    public class UserAccount : Dao
    {
        public decimal Coins { get; set; }

        public decimal Points { get; set; }

        [Required]
        public User User { get; set; }
    }
}
