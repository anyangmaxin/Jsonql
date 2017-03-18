using System;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Demo.AspNet
{
    public abstract class Dao
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}
