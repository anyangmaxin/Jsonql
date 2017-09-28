using System;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Sample.AspNetCore
{
    public class OrderStatusChange : Dao
    {
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public int Status { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
