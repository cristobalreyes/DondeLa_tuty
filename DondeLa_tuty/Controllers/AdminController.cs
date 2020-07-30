using DondeLa_tuty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using System.IO;

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
        public ActionResult convertirImagen(int ItemId)
        {
            Item imagenProducto = storeDB.Items.Where(x => x.ItemId == ItemId).FirstOrDefault();

            return File(imagenProducto.imagenProducto, "image/jpeg");
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
        public ActionResult CreateProduct([Bind(Include = "ItemId,CategoryId,ProducerID,Titulo,Precio")] Item item, HttpPostedFileBase imagenProducto)
        {
            if (imagenProducto != null && imagenProducto.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(imagenProducto.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imagenProducto.ContentLength);
                }
                //setear la imagen a la entidad que se creara

                item.imagenProducto = imageData;
            }
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
			ViewBag.ItemId = new SelectList(storeDB.Items, "ItemId", "Nombre", item.ItemId);
			return View(item);
        }

        // POST: StoreManager/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ItemId,CategoryId,ProducerID,Titulo,Precio,")] Item item, HttpPostedFileBase imagenProducto)
        {
            if (imagenProducto != null && imagenProducto.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(imagenProducto.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imagenProducto.ContentLength);
                }
                //setear la imagen a la entidad que se creara
                item.imagenProducto = imageData;
            }
            if (ModelState.IsValid)
            {
                storeDB.Entry(item).State = EntityState.Modified;
                storeDB.SaveChanges();
                return RedirectToAction("Product");
            }
            ViewBag.CategoryId = new SelectList(storeDB.Categories, "CategoryId", "Nombre", item.CategoryId);
            ViewBag.ProducerID = new SelectList(storeDB.Producers, "ProducerId", "Nombre", item.ProducerID);
			ViewBag.ItemId = new SelectList(storeDB.Items, "ItemId", "Nombre", item.ItemId);
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
        /// 
        /// <summary>
        /// CATEGORIAS CONTROLADORES
        /// </summary>
        /// <returns></returns>
        // GET: StoreManager/Create
        public ActionResult CreateCategory()
        { 

            return View();
        }

        // POST: StoreManager/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "CategoryId,Nombre,Descripcion")] Category Categories)
        {
            if (ModelState.IsValid)
            {
                storeDB.Categories.Add(Categories);
                storeDB.SaveChanges();
                return RedirectToAction("Categorias");
            }
       
            return View(Categories);
        }

        // GET: StoreManager/Edit/5
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Categories = storeDB.Categories.Find(id);
            if (Categories == null)
            {
                return HttpNotFound();
            }
            return View(Categories);
        }

        // POST: StoreManager/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "CategoryId,Nombre,Descripcion")] Category Categories)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(Categories).State = EntityState.Modified;
                storeDB.SaveChanges();
                return RedirectToAction("Categorias");
            }
  
            return View(Categories);
        }

        // GET: StoreManager/Delete/5
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Categories = storeDB.Categories.Find(id);
            if (Categories == null)
            {
                return HttpNotFound();
            }
            return View(Categories);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            Category Categories = storeDB.Categories.Find(id);
            storeDB.Categories.Remove(Categories);
            storeDB.SaveChanges();
            return RedirectToAction("Categorias");
        }
        /// 
        /// <summary>
        /// C CONTROLADORES
        /// </summary>
        /// <returns></returns>
        // GET: StoreManager/Create
        public ActionResult CreateProveedores()
        {

            return View();
        }

        // POST: StoreManager/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProveedores([Bind(Include = "CategoryId,Nombre,Descripcion")] Producer Proveedor)
        {
            if (ModelState.IsValid)
            {
                storeDB.Producers.Add(Proveedor);
                storeDB.SaveChanges();
                return RedirectToAction("Proveedores");
            }

            return View(Proveedor);
        }
        // GET: StoreManager/Edit/5
        public ActionResult EditProveedores(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer Proveedor = storeDB.Producers.Find(id);
            if (Proveedor == null)
            {
                return HttpNotFound();
            }
            return View(Proveedor);
        }

        // POST: StoreManager/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProveedores([Bind(Include = "ProducerId,Nombre")] Producer Proveedor)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(Proveedor).State = EntityState.Modified;
                storeDB.SaveChanges();
                return RedirectToAction("Proveedores");
            }

            return View(Proveedor);
        }
        // GET: StoreManager/Delete/5
        public ActionResult DeleteProveedores(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer Proveedor = storeDB.Producers.Find(id);
            if (Proveedor == null)
            {
                return HttpNotFound();
            }
            return View(Proveedor);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("DeleteProveedores")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProveedores(int id)
        {
            Producer Proveedor = storeDB.Producers.Find(id);
            storeDB.Producers.Remove(Proveedor);
            storeDB.SaveChanges();
            return RedirectToAction("Proveedores");
        }
    }
}