namespace MusicSystem.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using MusicSystem.Data;
    using MusicSystem.Data.Contracts;
    using MusicSystem.Models;
    using MusicSystem.Services.Models;

    public class AlbumsController : BaseController
    {
        public AlbumsController()
            : this(new MusicSystemData())
        {
        }

        public AlbumsController(IMusicSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var albums = this.Data.Albums
                             .All()
                             .Select(AlbumModel.FromAlbum);

            return this.Ok(albums);
        }

        [HttpPost]
        public IHttpActionResult Create(AlbumModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newAlbum = new Album
            {
                Title = album.Title,
                Year = album.Year,
            };

            this.Data.Albums.Add(newAlbum);
            this.Data.SaveChanges();

            album.Id = newAlbum.Id;

            return this.Ok(album);
        }

        [HttpPost]
        public IHttpActionResult AddArtist(int artistId, int albumId)
        {
            var artist = this.Data.Artists
                .All()
                .FirstOrDefault(ar => ar.Id == artistId);

            if (artist == null)
            {
                return BadRequest("Artist not found - probably invalid id");
            }

            var album = this.Data.Albums
                .All()
                .FirstOrDefault(al => al.Id == albumId);

            if (album == null)
            {
                return BadRequest("Album not found - probably invalid id");
            }

            album.Artists.Add(artist);
            this.Data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(int id, AlbumModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingAlbum = this.Data.Albums.All()
                                    .FirstOrDefault(al => al.Id == id);

            if (existingAlbum == null)
            {
                return this.BadRequest("Album not found - probably invalid id");
            }

            existingAlbum.Title = album.Title;
            existingAlbum.Year = album.Year;
            this.Data.SaveChanges();

            album.Id = existingAlbum.Id;

            return this.Ok(album);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var albumToDelete = this.Data.Albums.All().FirstOrDefault(a => a.Id == id);
            if (albumToDelete == null)
            {
                return this.BadRequest("Album not found - probably invalid id");
            }

            this.Data.Albums.Delete(albumToDelete);

            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}