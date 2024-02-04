using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMusicStoreWebAPI.Models;
using OnlineMusicStoreWebAPI.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineMusicStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly MusicDataAccess _musicDataAccess;

        public MusicController(MusicDataAccess musicDataAccess)
        {
            _musicDataAccess = musicDataAccess;
        }

        //Working
        // GET: api/Music
        [HttpGet]
        public ActionResult<IEnumerable<Music>> GetMusic()
        {
            return Ok(_musicDataAccess.GetAllMusic());
        }

        //Working
        // GET: api/Music/5
        [HttpGet("{id}")]
        public ActionResult<Music> GetMusic(int id)
        {
            var music = _musicDataAccess.GetMusicById(id);

            if (music == null)
            {
                return NotFound();
            }

            return Ok(music);
        }

        //Working
        // POST: api/Music
        [HttpPost]
        public ActionResult<Music> PostMusic(Music music)
        {
            _musicDataAccess.CreateMusic(music);

            return Ok(music);
        }

        //Working
        // PUT: api/Music/5
        [HttpPut("{id}")]
        public IActionResult PutMusic(int id, Music music)
        {
            if (id != music.Id)
            {
                return BadRequest();
            }

            _musicDataAccess.UpdateMusic(music);

            return NoContent();
        }
        //Working
        // DELETE: api/Music/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMusic(int id)
        {
            _musicDataAccess.DeleteMusic(id);

            return NoContent();
        }
    }
}
