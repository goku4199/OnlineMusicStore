using Microsoft.AspNetCore.Mvc;
using OnlineMusicStore.Models;
using OnlineMusicStore.Repository;
using System.Collections;
using System.Text;
using System.Text.Json;

namespace OnlineMusicStore.Controllers
{
    
public class MusicController : Controller
    {
        private HttpClient _client;

        public MusicController()
        {
            _client = new HttpClient();
        }

        //Working
        // GET: Music
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _client.GetAsync("https://localhost:7006/api/Music");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var music = JsonSerializer.Deserialize<IList<Music>>(data);
                return View(music);
            }
            return View(new List<Music>());
        }

        //Working
        // GET: Music/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Working
        // POST: Music/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Music music)
        {
            var content = new StringContent(JsonSerializer.Serialize(music), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("https://localhost:7006/api/music", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        //Working
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://localhost:7006/api/Music/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var music = JsonSerializer.Deserialize<Music>(data);
                return View(music);
            }
            return NotFound();
        }

        //Working
        // POST: Music/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, Music music)
        {
            if (id != music.id)
            {
                return BadRequest();
            }

            var content = new StringContent(JsonSerializer.Serialize(music), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync($"https://localhost:7006/api/music/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        //Working
        // POST: Music/Delete/5
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"https://localhost:7006/api/music/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }


}
