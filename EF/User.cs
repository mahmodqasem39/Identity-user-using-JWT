using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JWT.EF
{
    [Table("users")]
    public partial class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("userID")]
        public int UserId { get; set; }
        [Column("userName")]
        [StringLength(50)]
        public string? UserName { get; set; }
        [Column("Email")]
        [StringLength(50)]
        public string? Email { get; set; }
        [Column("PasswordSalt")]
        [StringLength(4000)]
        public string? PasswordSalt { get; set; }
        [Column("HashedPassword")]
        [StringLength(4000)]
        public string? HashedPassword { get; set; }
        [Column("fullName")]
        [StringLength(500)]
        public string? FullName { get; set; }

    }
}
