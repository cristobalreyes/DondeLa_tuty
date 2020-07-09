﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DondeLa_tuty.Models;

namespace DondeLa_tuty.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var category = new List<Category>
            {
                new Category {Name = "Frutas"},
                new Category {Name = "Verduras"}
            };
            return View(category);
        }

        public ActionResult Browse(string category)
        {
            var categoryModel = new Category { Name = category };
           return View(categoryModel);
        }
        public ActionResult Details(int id)
        {
            var Item = new Item { Titulo = "item"+ id};
            return View(Item);
        }
    }
}