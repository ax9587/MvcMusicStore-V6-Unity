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

namespace MvcMusicStore_V5.Controllers.Tests
{
    [TestClass()]
    public class GenresControllerTests
    {
        private IEnumerable<Genre> Genres { get; set; }
        [TestInitialize]
        public void Setup()
        {
            var stream = MusicData.GenreSampleData.ToStream();
            var reader = new StreamReader(stream);
            var serializer = new XmlSerializer(typeof(Collection<Genre>));
            Genres = (Collection<Genre>)serializer.Deserialize(reader);
        }



        [TestMethod()]
        public void IndexTest()
        {
            var mockContext = new Mock<MusicContext>();


            // Populate Customers "table"
            var genreDbSet = Genres.GetQueryableMockDbSet();
            foreach (var Genre in Genres)
            {
                genreDbSet.Add(Genre);
            }

            

            mockContext.Setup(context => context.Genres).Returns(genreDbSet);
           

            var dbContext = mockContext.Object;
            //var mockRepository = new Mock<IAlbumRepository>();
            //mockRepository.Setup(e => e.GetAll()).Returns(dbContext.Albums);
            //var AlbumRepository = mockRepository.Object;


            GenresController controller = new GenresController(dbContext);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var model = (List<Genre>)result.ViewData.Model;
            // Act


            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count() == 20);

           




        }


    }
}