﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using lab14.Models;
using System.Net;

namespace lab14.Controllers
{
    public class CategoriesController : Controller
    {

        #region Contexto
        //ATRIBUTO CONTEXTO
        private NorthwindEntities _contexto;
        //PROPIEDAD CONTEXTO
        public NorthwindEntities Contexto
        {
            set { _contexto = value; }
            get
            {
                if (_contexto == null)
                    _contexto = new NorthwindEntities();
                return _contexto;
            }
        }
        #endregion
        // GET: Categories
        public ActionResult Index()
        {
            
            return View(Contexto.Categories.ToList());
        }

        public ActionResult Details(int id)
        {
            var productosPorcategoria = from p in Contexto.Products
                                        orderby p.ProductName ascending

                                        where p.CategoryID == id

                                        select p;
            return View(productosPorcategoria.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Create(Categories nuevaCategoria)
        {
            try
            { //validamos los datos ingresados
                if (ModelState.IsValid)
                {
                    
                    Contexto.Categories.Add(nuevaCategoria);
                    Contexto.SaveChanges();
                    
                    return RedirectToAction("Index");

                }
                
                return View(nuevaCategoria);
            }
            catch
            {
              
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Categories CategoriaEditar = Contexto.Categories.Find(id);
            if (CategoriaEditar == null)
                return HttpNotFound();
            return View(CategoriaEditar);
        }
        //registrar cambios de la categoria en la BD
        [HttpPost]
        public ActionResult Edit(Categories CategoriaEditar)
        {
            try
            {//validamos los datos ingresados
                if (ModelState.IsValid)
                { 
                    Contexto.Entry(CategoriaEditar).State = System.Data.Entity.EntityState.Modified;
                    Contexto.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(CategoriaEditar);
            }
            catch
            { 
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Categories CategoriaEliminar = Contexto.Categories.Find(id);
            if (CategoriaEliminar == null)
                return HttpNotFound();

            return View(CategoriaEliminar);
        }
        // POST: /Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Categories Categoria1)
        {
            try
            {
                Categories CategoriaEliminar = new Categories();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    CategoriaEliminar = Contexto.Categories.Find(id);
                    if (CategoriaEliminar == null)
                        return HttpNotFound();

                    Contexto.Categories.Remove(CategoriaEliminar);

                    Contexto.SaveChanges();

                    return RedirectToAction("Index");

                }
                return View(CategoriaEliminar);
            }
            catch
            {
                return View();
            }
        }
    }

}