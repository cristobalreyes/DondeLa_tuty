using DondeLa_tuty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace DondeLa_tuty.Controllers
{
    public class AdminController : Controller
    {
        ShoppingStoreEntities storeDB = new ShoppingStoreEntities();
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
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

        // GET: StoreManager/Create
        public ActionResult CreateProduct()
        {
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre");
            return View();
        }

        // POST: StoreManager/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "ItemId,CategoriaId,ProducerID,Titulo,Precio,ItemArtUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                storeDB.Items.Add(item);
                storeDB.SaveChanges();
                return RedirectToAction("Index");
            }

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
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre", item.ProducerID);
            return View(item);
        }

        // POST: StoreManager/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ItemId,CategoriaId,ProducerID,Titulo,Precio,ItemArtUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(item).State = EntityState.Modified;
                storeDB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre", item.ProducerID);
            return View(item);
        }
    }
}