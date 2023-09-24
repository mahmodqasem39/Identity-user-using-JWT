using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JWT.EF
{
    [Table("courses")]
    public partial class Course
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CourseId")]
        public int CourseId { get; set; }
        [Column("CourseName")]
        [StringLength(50)]
        public string? CourseName { get; set; }
    }
}
