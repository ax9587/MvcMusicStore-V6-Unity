using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcMusicStore_V5.Controllers;
using MvcMusicStore_V5.Models;
using MvcMusicStore_V5.Tests.Resources;
using MvcMusicStore_V5.Tests.Controllers;
using MvcMusicStore_V5.Tests.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Web.Mvc;
using MvcMusicStore_V5.Tests.MusicRepositories;
using PagedList;

namespace MvcMusicStore_V5.Controllers.Tests
{
    [TestClass()]
    public class AlbumsControllerTests
    {
        
        [TestInitialize]
        public void Setup()
        {
            
        }



        [TestMethod()]
        public void AlbumsIndexTest()
        {


            AlbumsController controller = new AlbumsController(new StagingMusicsRepository());

            // Act
            ViewResult result = controller.Index("","","",0) as ViewResult;
            PagedList.PagedList<Album> model = (PagedList.PagedList<Album>)result.ViewData.Model;
            // Act


            // Assert
            Assert.IsNotNull(result);
            //  Assert.IsTrue(model.Count() == 2);

            //Route 
            //Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.AreEqual("SomeElse", result.["controller"]);

            ///Search Test
            // Act
            result = controller.Index("", "", "The Best", 0) as ViewResult;
            model =(PagedList.PagedList<Album>)result.ViewData.Model;
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.TotalItemCount== 1);

        }
        /*
        [TestMethod()]
        public void DetailsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }
        */
    }
}