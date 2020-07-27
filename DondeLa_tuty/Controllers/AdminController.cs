﻿using DondeLa_tuty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;


namespace DondeLa_tuty.Controllers
{
    [Authorize(Users = "Ali@gmail.com")]
    public class AdminController : Controller
    {

        ShoppingStoreEntities storeDB = new ShoppingStoreEntities();
        // GET: Admin
       
     
        
        public ActionResult Dashboard()
        {
            var items = storeDB.Items.Include(i => i.Producer);
            return View(items.ToList());
        }

        public ActionResult Categorias()
        {
            var categories = storeDB.Categories.ToList();
            return PartialView(categories);
        }

        public ActionResult Product()
        {
            var productos = storeDB.Items.ToList();
            return PartialView(productos);
        }

        public ActionResult Proveedores()
        {
            var proveedores = storeDB.Producers.ToList();
            return PartialView(proveedores);
        }

        // GET: StoreManager/Create
        public ActionResult CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(storeDB.Categories, "CategoryId", "Nombre");
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre");
            
            return View();
        }

        // POST: StoreManager/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "ItemId,CategoryId,ProducerID,Titulo,Precio,ItemArtUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                storeDB.Items.Add(item);
                storeDB.SaveChanges();
                return RedirectToAction("Product");
            }
            ViewBag.CategoryId = new SelectList(storeDB.Categories, "CategoryId", "Nombre", item.CategoryId);
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre", item.ProducerID);
            return View(item);
        }


        // GET: StoreManager/Edit/5
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = storeDB.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(storeDB.Categories, "CategoryId", "Nombre", item.CategoryId);
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre", item.ProducerID);
            return View(item);
        }

        // POST: StoreManager/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ItemId,CategoryId,ProducerID,Titulo,Precio,ItemArtUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(item).State = EntityState.Modified;
                storeDB.SaveChanges();
                return RedirectToAction("Product");
            }
            ViewBag.CategoryId = new SelectList(storeDB.Categories, "CategoryId", "Nombre", item.CategoryId);
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre", item.ProducerID);
            return View(item);
        }


        // GET: StoreManager/Delete/5
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = storeDB.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = storeDB.Items.Find(id);
            storeDB.Items.Remove(item);
            storeDB.SaveChanges();
            return RedirectToAction("Product");
        }
    }
}