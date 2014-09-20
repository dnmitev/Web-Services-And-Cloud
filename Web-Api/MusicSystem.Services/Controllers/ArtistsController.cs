namespace MusicSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using MusicSystem.Data;
    using MusicSystem.Data.Contracts;
    using MusicSystem.Models;
    using MusicSystem.Services.Models;

    public class ArtistsController : BaseController
    {
        public ArtistsController()
            : this(new MusicSystemData())
        {
        }

        public ArtistsController(IMusicSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var artists = this.Data.Artists.All()
                              .Select(ar => ArtistModel.FromArtist);

            return this.Ok(artists);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var artist = this.Data.Artists
                             .All()
                             .FirstOrDefault(ar => ar.Id == id);

            if (artist == null)
            {
                return this.BadRequest("Artist not found - probably invalid id");
            }

            return this.Ok(artist);
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var newArtist = new Artist
            {
                Name = artist.Name,
                Country = artist.Country,
                DateOfBirth = artist.DateOfBirth
            };

            this.Data.Artists.Add(newArtist);
            this.Data.SaveChanges();

            return this.Ok(artist);
        }

        public IHttpActionResult AddAlbum(int albumId, int artistId)
        {
            var artist = this.Data.Artists.All()
                             .FirstOrDefault(ar => ar.Id == artistId);

            if (artist == null)
            {
                return this.BadRequest("Artist  not found - probably invalid id");
            }

            var album = this.Data.Albums.All()
                            .FirstOrDefault(al => al.Id == albumId);

            if (album == null)
            {
                return this.BadRequest("Album not found - probably invalid id");
            }

            album.Artists.Add(artist);
            this.Data.SaveChanges();

            return this.Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ArtistModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var existingArtist = this.Data.Artists
                                     .All()
                                     .FirstOrDefault(ar => ar.Id == id);

            if (existingArtist == null)
            {
                return this.BadRequest("Invalid artist - probably invalid id");
            }

            existingArtist.Name = artist.Name;
            existingArtist.Country = artist.Country;
            existingArtist.DateOfBirth = artist.DateOfBirth;

            this.Data.SaveChanges();

            artist.Id = existingArtist.Id;

            return this.Ok(artist);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingArtist = this.Data.Artists
                                     .All()
                                     .FirstOrDefault(ar => ar.Id == id);

            if (existingArtist == null)
            {
                return this.BadRequest("Artist not found - probably invalid id");
            }

            this.Data.Artists.Delete(existingArtist);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}