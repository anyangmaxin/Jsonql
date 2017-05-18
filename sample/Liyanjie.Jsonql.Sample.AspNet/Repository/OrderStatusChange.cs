using System;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Sample.AspNet
{
    public class OrderStatusChange : Dao
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int Status { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
