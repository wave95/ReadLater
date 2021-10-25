using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class BookmarksController : Controller
    {
        IBookmarkService _bookmarsService;

        public BookmarksController(IBookmarkService bookmarksService)
        {
            _bookmarsService = bookmarksService;
        }

        // GET: Bookmarks
        public IActionResult Index()
        {
            List<BookmarkModel> model = _bookmarsService.GetBookmarks();
            return View(model);
        }

        // GET: Bookmarks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }

            BookmarkModel bookmark = _bookmarsService.GetBookmark((int)id);

            var request = Request;
            var _baseURL = $"{request.Scheme}://{request.Host}";
            var encoded = WebUtility.UrlEncode(bookmark.URL);
            bookmark.ShareUrl = string.Format("{0}/Bookmarks/SaveExternalClick/?extUrl={1}", _baseURL, encoded);

            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);

        }



        // GET: Bookmarks/Create
        public IActionResult Create()
        {
            var model = new BookmarkModel();
            model.CategoriesList = _bookmarsService.GetCategoriesList();               

            return View(model);
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookmarkModel bookmarkModel)
        {
            if (ModelState.IsValid)
            {
                _bookmarsService.CreateBookmark(bookmarkModel);
                return RedirectToAction("Index");
            }

            return View(bookmarkModel);
        }

        // GET: Bookmarks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            BookmarkModel bookmark = _bookmarsService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }

            bookmark.CategoriesList = _bookmarsService.GetCategoriesList();

            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookmarkModel bookmarkModel)
        {
            if (ModelState.IsValid)
            {
                _bookmarsService.UpdateBookmark(bookmarkModel);
                return RedirectToAction("Index");
            }
            return View(bookmarkModel);
        }

        [HttpPost]
        public IActionResult SaveClick(int bookmarkId)
        {
            if (ModelState.IsValid)
            {
                _bookmarsService.SaveClick(bookmarkId);

                return Ok();
            }
            else
            {
                return BadRequest();
            }                            
        }

        [HttpGet]
        public IActionResult SaveExternalClick([FromQuery] string extUrl)
        {
            if (ModelState.IsValid)
            {
                _bookmarsService.SaveClick(extUrl);

                return Redirect(extUrl);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: Bookmarks/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            BookmarkModel bookmarkModel = _bookmarsService.GetBookmark((int)id);
            if (bookmarkModel == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmarkModel);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookmarsService.DeleteBookmark(id);
            return RedirectToAction("Index");
        }
    }
}
