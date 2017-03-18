using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Demo.AspNetCore
{
    public class Order : Dao
    {
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public string Serial { get; set; } = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffffff");

        public int Status { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<OrderStatusChange> StatusChanges { get; set; }
    }
}
