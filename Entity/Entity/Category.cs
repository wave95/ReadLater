using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Category
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
       
    }
}

