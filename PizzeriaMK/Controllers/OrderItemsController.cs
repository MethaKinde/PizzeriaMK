﻿using PizzeriaMK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaMK.Controllers
{
    public class OrderItemsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: OrderItems
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var orderItems = db.OrderItems.Include(o => o.OrderSummary).Include(o => o.Product);
            return View(orderItems.ToList());
        }

        // GET: OrderItems/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // GET: OrderItems/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.OrderSummaryId = new SelectList(db.OrderSummaries, "OrderSummaryId", "OrderDate");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: OrderItems/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Quantity")] OrderItem orderItem, int id)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.Identity.Name;
                var summaryId = db.OrderSummaries.Where(o => o.UserId.ToString() == userId && o.State == "NON EVASO").FirstOrDefault();
                var product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();

                OrderItem newItem = new OrderItem();
                newItem.OrderSummaryId = summaryId.OrderSummaryId;
                newItem.ProductId = id;
                newItem.Quantity = orderItem.Quantity;
                newItem.ItemPrice = product.ProductPrice * (decimal)orderItem.Quantity;

                db.OrderItems.Add(newItem);
                db.SaveChanges();

                TempData["ProductSuccess"] = true;

                return RedirectToAction("Details", "Products", new { id = id });
            }

            return View(orderItem);
        }

        // GET: OrderItems/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderSummaryId = new SelectList(db.OrderSummaries, "OrderSummaryId", "OrderDate", orderItem.OrderSummaryId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderItemId,OrderSummaryId,ProductId,UserId,Quantity,ItemPrice")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderSummaryId = new SelectList(db.OrderSummaries, "OrderSummaryId", "OrderDate", orderItem.OrderSummaryId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            db.OrderItems.Remove(orderItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}