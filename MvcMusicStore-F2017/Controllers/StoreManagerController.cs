using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore_F2017.Models;

namespace MvcMusicStore_F2017.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        // db connection moved to Models/EFStoreManagerRepository.cs
        // private MusicStoreModel db = new MusicStoreModel();
        private IStoreManagerRepository db;

        // if no mock specified, use db
        public StoreManagerController()
        {
            this.db = new EFStoreManagerRepository();
        }

        // if we tell the controller we are testing, use the mock interface
        public StoreManagerController(IStoreManagerRepository db)
        {
            this.db = db;
        }

        // GET: StoreManager
        public ViewResult Index()
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            ViewBag.AlbumCount = albums.Count();
            return View(albums.OrderBy(a => a.Artist.Name).ThenBy(a => a.Title).ToList());
        }

        //// POST: Store/Manager - search by Title
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(string Title)
        //{
        //    // get only albums that contain the keyword(s) in the title
        //    var albums = from a in db.Albums
        //                 where a.Title.Contains(Title)
        //                 orderby a.Artist.Name, a.Title
        //                 select a;

        //    ViewBag.AlbumCount = albums.Count();
        //    ViewBag.SearchTerm = " - " + Title;
        //    return View(albums);
        //}

        [AllowAnonymous]
        // GET: StoreManager/Details/5
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Album album = db.Albums.FirstOrDefault(a => a.AlbumId == id);
            if (album == null)
            {
                return View("Error");
            }
            return View(album);
        }

        //// GET: StoreManager/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ArtistId = new SelectList(db.Artists.OrderBy(a => a.Name), "ArtistId", "Name");
        //    ViewBag.GenreId = new SelectList(db.Genres.OrderBy(g => g.Name), "GenreId", "Name");
        //    return View();
        //}

        //// POST: StoreManager/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // set the placeholder in case there is no upload
        //        album.AlbumArtUrl = "/Content/Images/placeholder.gif";

        //        // save new album cover if there is one
        //        if (Request.Files.Count > 0)
        //        {
        //            var file = Request.Files[0];

        //            if (file.FileName != null && file.ContentLength > 0)
        //            {
        //                string path = Server.MapPath("/Content/Images/") + file.FileName;
        //                file.SaveAs(path);
        //                album.AlbumArtUrl = "/Content/Images/" + file.FileName;
        //            }  
        //        }

        //        db.Albums.Add(album);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
        //    ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
        //    return View(album);
        //}

        //// GET: StoreManager/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Album album = db.Albums.Find(id);
        //    if (album == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ArtistId = new SelectList(db.Artists.OrderBy(a => a.Name), "ArtistId", "Name", album.ArtistId);
        //    ViewBag.GenreId = new SelectList(db.Genres.OrderBy(g => g.Name), "GenreId", "Name", album.GenreId);
        //    return View(album);
        //}

        //// POST: StoreManager/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // save new album cover if there is one
        //        if (Request.Files.Count > 0)
        //        {
        //            var file = Request.Files[0];

        //            if (file.FileName != null && file.ContentLength > 0)
        //            {
        //                string path = Server.MapPath("/Content/Images/") + file.FileName;
        //                file.SaveAs(path);
        //                album.AlbumArtUrl = "/Content/Images/" + file.FileName;
        //            }
        //        }

        //        db.Entry(album).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
        //    ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
        //    return View(album);
        //}

        //// GET: StoreManager/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Album album = db.Albums.Find(id);
        //    if (album == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(album);
        //}

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ViewResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            Album album = db.Albums.FirstOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return View("Error");
            }

            db.Delete(album);
            return View("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
