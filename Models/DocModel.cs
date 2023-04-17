using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Text_Editor.Models
{
    public class DocModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? DocName { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        public string? DocContent { get; set; }

        [Required]
        public string? IsSharable { get; set; }
    }
}
