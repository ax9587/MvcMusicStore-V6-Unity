using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcMusicStore_V5.Controllers;
using MvcMusicStore_V5.Models;
using MvcMusicStore_V5Tests.Controllers;
using MvcMusicStore_V5Tests.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MvcMusicStore_V5.Controllers.Tests
{
    [TestClass()]
    public class AlbumsControllerTests
    {
        private IEnumerable<Album> albums { get; set; }
        [TestInitialize]
        public void Setup()
        {
            var stream = MusicData.MusicSampleData.ToStream();
            var reader = new StreamReader(stream);
            var serializer = new XmlSerializer(typeof(Collection<Album>));
            albums = (Collection<Album>)serializer.Deserialize(reader);
        }



        [TestMethod()]
        public void IndexTest()
        {
            var mockContext = new Mock<MvcMusicStore_V5.Models.MusicContext>();

            Assert.Fail();
        }

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
    }
}