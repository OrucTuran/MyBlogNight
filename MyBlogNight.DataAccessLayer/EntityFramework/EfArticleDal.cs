﻿using Microsoft.EntityFrameworkCore;
using MyBlogNight.DataAccessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.DataAccessLayer.Repositories;
using MyBlogNight.DtoLayer.Dtos.ArticleDtos;
using MyBlogNight.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogNight.DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>, IArticleDal
    {
        public EfArticleDal(BlogContext context) : base(context)
        {
        }

        public List<Article> ArticleListWithCategory()
        {
            var context = new BlogContext();
            var values = context.Articles.Include(x => x.Category).ToList();
            return values;
        }
        public List<Article> ArticleListWithCategoryAndAppUser()
        {
            var context = new BlogContext();
            var values = context.Articles.Include(x => x.Category).Include(y => y.AppUser).ToList();
            return values;
        }

        public Article ArticleListWithCategoryAndAppUserByArticleId(int id)
        {
            var context = new BlogContext();
            var values = context.Articles.Where(x => x.ArticleId == id).Include(y => y.Category).Include(z => z.AppUser).FirstOrDefault();
            return values;
        }

        public void ArticleViewCountIncrease(int id)
        {
            var context = new BlogContext();
            var updatedValue = context.Articles.Find(id);
            updatedValue.ArticleViewCount += 1;
            context.SaveChanges();
        }

        public List<Article> GetArticlesByAppUserId(int id)
        {
            var context = new BlogContext();
            var values = context.Articles.Where(x => x.AppUserId == id).ToList();
            return values;
        }

        public List<Article> GetArticlesByViewCount()
        {
            var context = new BlogContext();

            // ArticleViewCount'a göre sıralama yapıyoruz (en çok görüntülenen makale önce gelecek)
            var sortedArticles = context.Articles
                .OrderByDescending(a => a.ArticleViewCount) // ArticleViewCount'a göre azalan sıralama
                .Take(4)
                .Include(a => a.Category) // İlgili kategoriyi de dahil ediyoruz
                .Include(a => a.AppUser)  // İlgili kullanıcıyı da dahil ediyoruz
                .ToList();

            return sortedArticles;
        }

        public List<CategoryWithArticleCount> GetCategoriesWithArticleCount()
        {
            var context = new BlogContext();

            // Kategorileri ve her kategorinin makale sayısını alıyoruz
            var categoryCounts = context.Categories
                .Select(c => new CategoryWithArticleCount
                {
                    CategoryName = c.CategoryName,
                    ArticleCount = c.Articles.Count
                })
                .OrderByDescending(c => c.ArticleCount)  // En çok makale bulunan kategori en üstte olacak
                .ToList();

            return categoryCounts;
        }
    }
}
