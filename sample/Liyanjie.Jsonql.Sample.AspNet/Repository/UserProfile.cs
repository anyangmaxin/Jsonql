using System.ComponentModel.DataAnnotations;

namespace Liyanjie.Jsonql.Sample.AspNet
{
    public class UserProfile : Dao
    {
        [MaxLength(50)]
        public string Avatar { get; set; }

        [MaxLength(50)]
        public string Nick { get; set; }

        [Required]
        public User User { get; set; }
    }
}
