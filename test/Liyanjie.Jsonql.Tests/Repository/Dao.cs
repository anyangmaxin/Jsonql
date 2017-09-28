using System;
using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Sample.AspNetCore
{
    public abstract class Dao
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}
