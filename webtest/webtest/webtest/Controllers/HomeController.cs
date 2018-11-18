﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtest.Models;

namespace webtest.Controllers
{
    public class HomeController : Controller
    {
        //Connectie met database
        DatabaseEntities1 db = new DatabaseEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WishList()
        {
            ViewBag.Message = "THIS IS THE WISHLIST PAGE";
            return View();

        }

        public ActionResult ShoppingCart(string isbn, bool delete = false, bool plus = false, bool min = false)
        {
            List<Book> bookList = new List<Book>();
            if (Session["User_id"] != null)
            {
                int User_id = Convert.ToInt32(Session["User_id"]);
                var result = (from cart in db.Carts
                              from book in db.Books
                              from user in db.Users
                              where cart.ISBN == book.ISBN &&
                              user.User_id == User_id
                              select book).ToList();

                return View(result);
            }
            else
            {
                if (Session["shoppingCart"] != null)
                {
                    List<string> isbns = Session["shoppingCart"].ToString().Split(',').ToList();
                    var total = 0;


                    //Deletes product from cart
                    if (isbns.Contains(isbn) && delete)
                    {
                        isbns.Remove(isbn);
                        var newcart = String.Join(",", isbns);
                        Session["shoppingCart"] = newcart;

                        delete = false;
                    }

                    if (plus)
                    {
                        isbns.Add(isbn);
                        var newcart = String.Join(",", isbns);
                        Session["shoppingCart"] = newcart;

                        plus = false;
                    }

                    if (min)
                    {
                        isbns.Remove(isbn);
                        var newcart = String.Join(",", isbns);
                        Session["shoppingCart"] = newcart;

                        plus = false;
                    }

                    try
                    {
                        foreach (string x in isbns)
                        {
                            double add = Convert.ToDouble(x);

                            //Voegt de boeken uit de isbn lijst toe aan de lijst voor de view.
                            bookList.Add(db.Books.Where(m => m.ISBN == add).FirstOrDefault());

                            //Checkt of het boek al getoont wordt.
                            if (bookList.GroupBy(m => m.ISBN).Any(c => c.Count() > 1))
                            {
                                bookList.Remove(db.Books.Where(m => m.ISBN == add).FirstOrDefault());
                            }

                            // Berekent de totale prijs van alle boeken.
                            total = total + Decimal.ToInt32(db.Books.Where(m => m.ISBN == add).Sum(m => m.Price));

                            // Berekent het aantal van het gekozen boek.
                            var bookAmount = 0;
                            foreach (var y in isbns)
                            {
                                if (y == x)
                                {
                                    bookAmount = bookAmount + 1;
                                }
                            }
                            ViewData[x] = bookAmount;

                        }

                        ViewBag.totalPrice = total;
                    }
                    catch (FormatException)
                    {

                    }


                }
            }


            return View(bookList);

        }

        public ActionResult OrderStatus()
        {
            ViewBag.Message = "THIS IS THE ORDER STATUS PAGE";

            if (Session["User_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View();
            }
        }
    }
}