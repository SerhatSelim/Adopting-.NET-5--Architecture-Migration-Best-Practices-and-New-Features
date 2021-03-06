﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookApp.DAL;
using BookApp.Models;

namespace BookApp.Web.Controllers
{
    public class BookReviewsController : Controller
    {
        private IBookRepository bookRepository;

        public BookReviewsController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        // GET: BookReviews
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


        // GET: BookReviews/Create
        public ActionResult Create(int bookId)
        {
            var book = bookRepository.GetBookById(bookId);
            return View(new BookReview { Book = book });
        }

        // POST: BookReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int bookId,[Bind(Include = "Rating,Review,Title")] BookReview bookReview)
        {

            if (ModelState.IsValid)
            {
                bookRepository.AddReviewToBook(bookId, bookReview);
                return RedirectToAction("Index",new { id = bookId});
            }

            return View(bookReview);
        }

        // GET: BookReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReview bookReview = bookRepository.GetBookReviewById(id);
            if (bookReview == null)
            {
                return HttpNotFound();
            }
            return View(bookReview);
        }

        // POST: BookReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int bookId,[Bind(Include = "Id,Rating,Review,Title")] BookReview bookReview)
        {
            if (ModelState.IsValid)
            {
                bookRepository.UpdateBookReview(bookReview);
                return RedirectToAction("Index", new { id = bookId });
            }
            return View(bookReview);
        }

        // GET: BookReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReview bookReview = bookRepository.GetBookReviewById(id);
            if (bookReview == null)
            {
                return HttpNotFound();
            }
            return View(bookReview);
        }

        // POST: BookReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int bookId)
        {
            bookRepository.DeleteBookReview(id);
            return RedirectToAction("Index", new { id = bookId });
        }

    }
}
