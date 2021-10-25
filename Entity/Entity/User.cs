
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entity
{
    public class User : IdentityUser
    {
        public ICollection<Category> Categories  { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<UserBookmarkClick> UserBookmarkClicks { get; set; }

    }
}
