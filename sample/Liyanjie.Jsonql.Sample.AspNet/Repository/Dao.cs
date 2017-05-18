using System;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Sample.AspNet
{
    public abstract class Dao
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}
