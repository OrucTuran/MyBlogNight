﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.BusinessLayer.ValidationRules.CategoryValidationRules;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class CategoryController : Controller //ADMIN TARAFI ICIN KULLANILACAK
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult CategoryList()
        {
            var values = _categoryService.TGetAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            ModelState.Clear(); //mevcuttaki butun data annotacionslari siler. bunun yerine asagidaki benim kurallarim gecerli olur.
            CreateCategoryValidator validationRules = new CreateCategoryValidator();
            ValidationResult result = validationRules.Validate(category); //category den gelen degerleri validate et
            if (result.IsValid) //validasyona takilmiyorsa
            {
                _categoryService.TInsert(category);
                return RedirectToAction("CategoryList");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName/*hataya sebep olan property*/, item.ErrorMessage);
                }
                return View();
            }

        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryService.TDelete(id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _categoryService.TGetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.TUpdate(category);
            return RedirectToAction("CategoryList");
        }
    }
}