using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// required references
using System.Web.Mvc;
using MvcMusicStore_F2017.Controllers;
using Moq;
using MvcMusicStore_F2017.Models;

namespace MvcMusicStore_F2017.Tests.Controllers
{
    [TestClass]
    public class StoreManagerControllerTest
    {
        // globals
        StoreManagerController controller;
        Mock<IStoreManagerRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            mock = new Mock<IStoreManagerRepository>();

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
    }
}
