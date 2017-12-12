using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMusicStore_F2017;
using MvcMusicStore_F2017.Controllers;

namespace MvcMusicStore_F2017.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWeatherColdValid()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            string result = controller.GetWeather(-15);

            // Assert
            Assert.AreEqual("Stay home", result);
        }

        // this test isn't really necessary as all code paths will be tested anyway
        [TestMethod]
        public void GetWeatherColdInvalid()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            string result = controller.GetWeather(-15);

            // Assert
            Assert.AreNotEqual("Go fishing", result);
        }

        [TestMethod]
        public void GetWeatherMediumValid()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            string result = controller.GetWeather(15);

            // Assert
            Assert.AreEqual("Go fishing", result);
        }

        [TestMethod]
        public void GetWeatherHotValid()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            string result = controller.GetWeather(25);

            // Assert
            Assert.AreEqual("Go play beach volleyball", result);
        }
    }
}
