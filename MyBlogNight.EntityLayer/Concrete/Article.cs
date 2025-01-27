﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.EntityLayer.Concrete
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CoverImageUrl { get; set; }
        public string MainImageUrl { get; set; }
        public int CategoryId { get; set; }// bu makale hangi kategori
        public Category Category { get; set; }
        public int AppUserId { get; set; } //bu makaleyi kim yazdi
        public AppUser AppUser { get; set; }
        public string Detail { get; set; }
        public int? ArticleViewCount { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
