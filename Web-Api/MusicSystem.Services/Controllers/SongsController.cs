namespace MusicSystem.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using MusicSystem.Data;
    using MusicSystem.Data.Contracts;
    using MusicSystem.Models;
    using MusicSystem.Services.Models;

    public class SongsController : BaseController
    {
        public SongsController() : this(new MusicSystemData())
        {
        }

        public SongsController(IMusicSystemData data) : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var songs = this.Data.Songs
                            .All()
                            .Select(SongModel.FromSong);

            return this.Ok(songs);
        }

        [HttpPost]
        public IHttpActionResult Create(SongModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var newSong = new Song
            {
                Title = song.Title,
                Year = song.Year,
                Genre = song.Genre
            };

            this.Data.Songs.Add(newSong);
            this.Data.SaveChanges();

            song.Id = newSong.Id;
            return this.Ok(song);
        }

        [HttpPost]
        public IHttpActionResult AddArtist(int artistId, int songId)
        {
            var artist = this.Data.Artists
                             .All()
                             .FirstOrDefault(ar => ar.Id == artistId);

            if (artist == null)
            {
                return BadRequest("Artist not found - probably invalid id");
            }

            var song = this.Data.Songs
                           .All()
                           .FirstOrDefault(s => s.Id == songId);

            if (song == null)
            {
                return BadRequest("Song not found - probably invalid id");
            }

            song.Artist = artist;
            this.Data.SaveChanges();

            return Ok(song);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SongModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var songToUpdate = this.Data.Songs
                                   .All()
                                   .FirstOrDefault(s => s.Id == id);

            if (songToUpdate == null)
            {
                return this.BadRequest("Song not found - probably invalid id");
            }

            songToUpdate.Title = song.Title;
            songToUpdate.Year = song.Year;
            songToUpdate.Genre = song.Genre;

            this.Data.SaveChanges();

            song.Id = songToUpdate.Id;

            return this.Ok(song);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var songToDelete = this.Data.Songs
                                   .All()
                                   .FirstOrDefault(s => s.Id == id);

            if (songToDelete == null)
            {
                return this.BadRequest("Song not found - probably invalid id");
            }

            this.Data.Songs.Delete(songToDelete);
            this.Data.SaveChanges();

            return this.Ok();
        }
    }
}