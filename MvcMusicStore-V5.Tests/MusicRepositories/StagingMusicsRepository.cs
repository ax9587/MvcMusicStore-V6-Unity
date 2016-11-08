using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMusicStore_V5.Models;
using MvcMusicStore_V5.Tests.Resources;
using System.IO;
using System.Xml.Serialization;
using MvcMusicStore_V5.Tests.Controllers;
using System.Collections.ObjectModel;

namespace MvcMusicStore_V5.Tests.MusicRepositories
{
   public class StagingMusicsRepository : IMusicsRepository
    {
        private IEnumerable<Album> albums { get; set; }

        public StagingMusicsRepository()
        { 
            var stream = MusicData.MusicSampleData.ToStream();
            var reader = new StreamReader(stream);
            var serializer = new XmlSerializer(typeof(Collection<Album>));
            albums = (Collection<Album>)serializer.Deserialize(reader) ;
        }
        public List<Album> GetAllAlbums()
        {
            
            return albums.ToList();
        }
    }
}
