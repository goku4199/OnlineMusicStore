using Microsoft.AspNetCore.Mvc;
using OnlineMusicStore.Models;
using OnlineMusicStore.Repository;

namespace OnlineMusicStore.Controllers
{
    public class MusicController : Controller
    {
        //private readonly MusicDataAccess musicDataAccess;
        private readonly MusicDataAccess musicDataAccess;

        public MusicController(MusicDataAccess musicDataAccess)
        {
            this.musicDataAccess = musicDataAccess;
        }

        /*public MusicController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("YourConnectionStringName");
            musicDataAccess = new MusicDataAccess(connectionString);
        }*/

        //Funtionality Created and tested
        [HttpGet]
        public IActionResult Index()
        {
            var musicList = musicDataAccess.GetAllMusic();
            return View(musicList);
        }

        //Funtionality Created and tested
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Functionality Created and Tested
        [HttpPost]
        public IActionResult Create(Music music)
        {
            musicDataAccess.CreateMusic(music);
            return RedirectToAction("Index");
        }

        /*
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var music = musicDataAccess.GetMusicById(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }

        */

        //Funtionality Created and tested
        [HttpGet]
        public IActionResult Update(int id)
        {
            // Retrieve the Music record from the database based on the provided id
            Music music = musicDataAccess.GetMusicById(id);

            // Check if the music record exists
            if (music == null)
            {
                return NotFound(); // Or handle the case where the record is not found
            }

            // Pass the Music object to the Edit.cshtml Razor view for user input
            return View(music);
        }

        //Funtionality Created and tested
        [HttpPost]
        public IActionResult Update(Music music)
        {
            musicDataAccess.UpdateMusic(music);
            return RedirectToAction("Index");
        }
        /*
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var music = musicDataAccess.GetMusicById(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }
        */

        [HttpPost]
        public IActionResult DeleteMusic(int id)
        {
            musicDataAccess.DeleteMusic(id);
            return RedirectToAction("Index");
        }
    }

}
