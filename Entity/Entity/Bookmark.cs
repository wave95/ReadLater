using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Bookmark
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
  
        [Key]
        [Required]
        public int ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL { get; set; }

        [StringLength(maximumLength: 50)]
        public string ShortDescription { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public DateTime CreateDate { get; set; }

    }
        
}
