using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApplication1_17286.Models;

public class SongController : Controller
{
    private readonly string _baseUrl = "http://ec2-34-200-212-116.compute-1.amazonaws.com:44312/"; // Base URL for API

    // GET: Song
    // Retrieves a list of songs and displays them on the main index page.
    public async Task<IActionResult> Index()
    {
        List<Song> songInfo = new List<Song>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Sends a GET request to the API to retrieve song data.
            HttpResponseMessage response = await client.GetAsync("api/Song");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                songInfo = JsonConvert.DeserializeObject<List<Song>>(responseContent);
            }

            return View(songInfo); // Passes the list of songs to the view.
        }
    }

    // GET: Song/Details/{id}
    // Retrieves details of a specific song by ID and displays them.
    public async Task<IActionResult> Details(int id)
    {
        Song song = await GetSongByIdAsync(id);
        if (song == null)
        {
            return NotFound(); // Returns 404 if the song is not found.
        }
        return View(song); // Passes the song details to the view.
    }

    // GET: Song/Create
    // Displays a form to create a new song.
    public IActionResult Create()
    {
        return View();
    }

    // POST: Song/Create
    // Handles the form submission for creating a new song.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Song song)
    {
        if (!ModelState.IsValid)
        {
            return View(song); // Returns the form with errors if model validation fails.
        }

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serializes the song object to JSON and sends it in a POST request.
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Song", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirects to the index on success.
            }
        }
        return View(song); // If creation fails, redisplay the form.
    }

    // GET: Song/Edit/{id}
    // Retrieves the song data by ID to prefill the edit form.
    public async Task<IActionResult> Edit(int id)
    {
        Song song = await GetSongByIdAsync(id);
        if (song == null)
        {
            return NotFound(); // Returns 404 if the song is not found.
        }
        return View(song); // Passes the song to the edit view.
    }

    // POST: Song/Edit/{id}
    // Handles the form submission for editing an existing song.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Song song)
    {
        if (!ModelState.IsValid || id != song.Id)
        {
            return View(song); // Returns the form with errors if validation fails or ID mismatch.
        }

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serializes the song object to JSON and sends it in a PUT request.
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"api/Song/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirects to the index on success.
            }
        }
        return View(song); // If update fails, redisplay the form.
    }

    // GET: Song/Delete/{id}
    // Retrieves the song data by ID to confirm deletion.
    public async Task<IActionResult> Delete(int id)
    {
        Song song = await GetSongByIdAsync(id);
        if (song == null)
        {
            return NotFound(); // Returns 404 if the song is not found.
        }
        return View(song); // Passes the song to the delete confirmation view.
    }

    // POST: Song/Delete/{id}
    // Handles the deletion of a song after confirmation.
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Sends a DELETE request to remove the song from the database.
            HttpResponseMessage response = await client.DeleteAsync($"api/Song/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirects to the index on success.
            }
        }
        return NotFound(); // Returns 404 if deletion fails.
    }

    // Helper method to retrieve a song by ID from the API.
    private async Task<Song> GetSongByIdAsync(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Sends a GET request to retrieve a specific song by ID.
            HttpResponseMessage response = await client.GetAsync($"api/Song/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Song>(responseContent); // Deserializes the JSON response to a Song object.
            }
        }
        return null; // Returns null if the song is not found.
    }
}
