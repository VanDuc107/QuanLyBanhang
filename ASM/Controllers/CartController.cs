using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ASM_NET104.Models;
using ASM_NET104.ViewModels;
using ASM.Extensions;

namespace ASM_NET104.Controllers
{
    public class CartController : Controller
    {
        private readonly ASM_NET104Context _context;
        private static List<ProductViewModel> products;

        public CartController(ASM_NET104Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var getAll = _context.Sanpham.ToList();
            var cartItem = SessionHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");
            products = new List<ProductViewModel>();
            if (cartItem != null)
            {
                foreach (var item in cartItem)
                {
                    var getProducts = getAll.Single(x => x.MaSp.Equals(item.ProductID));
                    products.Add(new ProductViewModel(item.ProductID, item.Quantity, getProducts.TenSp, getProducts.DonGia, getProducts.HinhAnh));
                }
            }
            return View(products);
        }
        public IActionResult Remove(int id)
        {
            List<CartItem> cartItems = SessionHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");

            var findItem = cartItems.SingleOrDefault(x => x.ProductID.Equals(id));
            if (findItem != null)
            {
                cartItems.Remove(findItem);
                SessionHelper.SetObjAsJson(HttpContext.Session, "cart", cartItems);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Checkout()
        {
            List<CartItem> cartItems = SessionHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cartItems != null)
            {
                List<ChiTietHd> chiTietHDs = new List<ChiTietHd>();
                foreach (var item in products)
                {
                    chiTietHDs.Add(new ChiTietHd()
                    {
                        Count = item.Quantity,
                        MaSp = item.ProductID,
                        Price = Convert.ToDouble(item.Price),
                        Total = Convert.ToDouble(item.Total)
                    });
                }

                HoaDon hd = new HoaDon() { TotalPrice = chiTietHDs.Sum(x => x.Total) };
                _context.HoaDon.Add(hd);
                _context.SaveChanges();
                var idHD = _context.HoaDon.ToList().Last().Id;
                foreach (var item in chiTietHDs)
                {
                    item.IdhoaDon = idHD;
                    _context.ChiTietHd.Add(item);
                }
                _context.SaveChanges();


                SessionHelper.SetObjAsJson(HttpContext.Session, "cart", null);
                return View();
            }
            return View();
        }
        public IActionResult AddCart(int id)
        {
            List<CartItem> cartItem = SessionHelper.GetObjFromJson<List<CartItem>>(HttpContext.Session, "cart");
            //Empty cart
            if (cartItem == null)
            {
                cartItem = new List<CartItem>();
                //Addsession
                cartItem.Add(new CartItem() { ProductID = id, Quantity = 1 });
                SessionHelper.SetObjAsJson(HttpContext.Session, "cart", cartItem);
            }
            else
            {
                bool isAdded = false;
                //Get cart
                for (int i = 0; i < cartItem.Count(); i++)
                {
                    if (cartItem[i].ProductID.Equals(id))
                    {
                        cartItem[i].Quantity++;
                        isAdded = true;
                    }
                }

                if (!isAdded)
                {
                    cartItem.Add(new CartItem() { ProductID = id, Quantity = 1 });
                }

                SessionHelper.SetObjAsJson(HttpContext.Session, "cart", cartItem);
            }

            return RedirectToAction("Index", "Home");
        }
    }
    public class CartItem
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
