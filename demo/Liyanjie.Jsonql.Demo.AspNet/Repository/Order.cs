using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Demo.AspNet
{
    public class Order : Dao
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Serial { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

        public int Status { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<OrderStatusChange> StatusChanges { get; set; }
    }
}
