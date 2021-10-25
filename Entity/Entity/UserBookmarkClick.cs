using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class UserBookmarkClick
    {
        [Key]
        public Guid UID { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int BookmarkId { get; set; }
        public virtual Bookmark Bookmark { get; set; }

        public DateTime ClickedOn { get; set; }

        public bool ExternalClick { get; set; }
    }
}
