using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMusicStore_F2017.Models
{
    // repository for mock album data for unit testing
    public interface IStoreManagerRepository
    {
        IQueryable<Album> Albums { get; }
        IQueryable<Artist> Artists { get;  }
        IQueryable<Genre> Genres { get; }
        Album Save(Album album);
        void Delete(Album album);
    }
}
