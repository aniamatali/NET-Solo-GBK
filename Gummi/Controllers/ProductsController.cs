using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gummi.Models;

namespace Gummi.Controllers
{
	public class ProductsController : Controller
	{
		private GummiDbContext db = new GummiDbContext();
		public IActionResult Index()
		{
			return View(db.Products.ToList());
		}


        public IActionResult Details(int id)
        {
            var thisProduct = db.Products
                                 .Include(x => x.Reviews)
                                 .FirstOrDefault(items => items.ProductId == id);

            return View(thisProduct);
        }


		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Product item)
		{
			db.Products.Add(item);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Edit(int id)
		{
			var thisProduct = db.Products.FirstOrDefault(items => items.ProductId == id);
			return View(thisProduct);
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			db.Entry(product).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var thisProduct = db.Products.FirstOrDefault(items => items.ProductId == id);
			return View(thisProduct);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var thisProduct = db.Products.FirstOrDefault(items => items.ProductId == id);
			db.Products.Remove(thisProduct);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
