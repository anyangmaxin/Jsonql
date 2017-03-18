using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Demo.AspNetCore
{
    public class UserAccount : Dao
    {
        public decimal Coins { get; set; }

        public decimal Points { get; set; }

        [Required]
        public User User { get; set; }
    }
}
