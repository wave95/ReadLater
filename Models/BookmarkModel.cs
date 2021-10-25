using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BookmarkModel
    {
        public int ID { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters only")]
        public string URL { get; set; }
        
        [Display(Name = "Description")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters only")]
        public string ShortDescription { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Created on")]
        public DateTime CreateDate { get; set; }

        public CategoryModel Category { get; set; }

        public string ShareUrl { get; set; }

        public List<CategoryModel> CategoriesList { get; set; }
    }
}
