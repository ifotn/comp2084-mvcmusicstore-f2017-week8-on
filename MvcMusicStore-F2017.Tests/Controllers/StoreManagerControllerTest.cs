using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// required references
using System.Web.Mvc;
using MvcMusicStore_F2017.Controllers;
using Moq;
using MvcMusicStore_F2017.Models;
using System.Linq;

namespace MvcMusicStore_F2017.Tests.Controllers
{
    [TestClass]
    public class StoreManagerControllerTest
    {
        // globals
        StoreManagerController controller;
        Mock<IStoreManagerRepository> mock;
        List<Album> albums;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            mock = new Mock<IStoreManagerRepository>();

            // mock Album data
            albums = new List<Album>
            {
                new Album { AlbumId = 1, Title = "Album 1", Price = 8,
                    Artist = new Artist { ArtistId = 1, Name = "Artist 1"} },
                new Album { AlbumId = 2, Title = "Album 2", Price = 10,
                    Artist = new Artist { ArtistId = 2, Name = "Artist 2"} },
                new Album { AlbumId = 3, Title = "Album 3", Price = 9,
                    Artist = new Artist { ArtistId = 3, Name = "Artist 3"} }
            };

            // add Album data to the mock object
            mock.Setup(m => m.Albums).Returns(albums.AsQueryable());

            // pass the mock to the controller
            controller = new StoreManagerController(mock.Object);
        }

        [TestMethod]
        public void IndexLoadsValid()
        {
            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexShowsValidAlbums()
        {
            // act
            var actual = (List<Album>)controller.Index().Model;

            // assert
            CollectionAssert.AreEqual(albums, actual);
        }

        [TestMethod]
        public void DetailsValidAlbum()
        {
            // act
            var actual = (Album)controller.Details(1).Model;

            // assert
            Assert.AreEqual(albums.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            ViewResult actual = controller.Details(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidNoId()
        {
            // act
            ViewResult actual = controller.Details(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedNoId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(1);

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }

        // GET: Edit
        [TestMethod]
        public void EditInvalidNoId()
        {
            // Arrange
            int? id = null;

            // Act          
            ViewResult actual = controller.Edit(id);

            //Assert           
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditValidId()
        {
            // act           
            ViewResult actual = controller.Edit(1);

            // assert           
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditInvalidId()
        {
            // act           
            ViewResult actual = controller.Edit(4);

            // assert   
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditValidArtists()
        {
            // act
            ViewResult actual = controller.Edit(1);

            // assert
            Assert.IsNotNull(actual.ViewBag.ArtistId);
        }

        [TestMethod]
        public void EditValidGenres()
        {
            // act
            ViewResult actual = controller.Edit(1);

            // assert
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

        // POST: Edit
        [TestMethod]
        public void EditSaveValid()
        {
            // act - casting the ActionResult to a RedirectToRouteResult
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(albums[0]);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditSaveInvalidModelArtists()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Edit(albums[0]);

            // assert
            Assert.IsNotNull(actual.ViewBag.ArtistId);
        }

        [TestMethod]
        public void EditSaveInvalidModelGenres()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Edit(albums[0]);

            // assert
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

        [TestMethod]
        public void EditSaveInvalidLoadsEditView()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Edit(albums[0]);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        // GET: Delete
        [TestMethod]
        public void DeleteNoId()
        {
            // Act           
            ViewResult actual = controller.Delete(null);

            // Assert           
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteValidId()
        {
            //act             
            ViewResult actual = controller.Delete(1);

            //assert             
            Assert.AreEqual("Delete", actual.ViewName);
        }

        [TestMethod]
        public void DeleteInValidId()
        {
            //act 
            ViewResult actual = controller.Delete(4);

            //assert 
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Create
        [TestMethod]
        public void CreateLoadsArtists()
        {
            // act
            ViewResult actual = (ViewResult)controller.Create();

            // assert
            Assert.IsNotNull(actual.ViewBag.ArtistId);
        }

        [TestMethod]
        public void CreateLoadsGenres()
        {
            // act
            ViewResult actual = (ViewResult)controller.Create();

            // assert
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

        [TestMethod]
        public void CreateLoadsView()
        {
            // act
            ViewResult actual = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Create
        [TestMethod]
        public void CreateSaveValid()
        {
            // act - casting the ActionResult to a RedirectToRouteResult
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Create(albums[0]);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateSaveInvalidModelArtists()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Create(albums[0]);

            // assert
            Assert.IsNotNull(actual.ViewBag.ArtistId);
        }

        [TestMethod]
        public void CreateSaveInvalidModelGenres()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Create(albums[0]);

            // assert
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

        [TestMethod]
        public void CreateSaveInvalidLoadsEditView()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Create(albums[0]);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }
    }
}
