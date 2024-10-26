using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1_17286.Models;

public class SongController : Controller
{
    private readonly string _baseUrl = "https://localhost:44312/"; // Base URL for API

    // GET: Song
    public async Task<IActionResult> Index()
    {
        List<Song> songInfo = new List<Song>(); // List to store songs
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear existing headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            HttpResponseMessage response = await client.GetAsync("api/Song"); // Send GET request

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); // Read response as string
                songInfo = JsonConvert.DeserializeObject<List<Song>>(responseContent); // Deserialize JSON response
            }

            return View(songInfo); // Return view with songs
        }
    }

    // GET: Song/{songId}
    public async Task<IActionResult> Details(int id)
    {
        Song song = null; // Placeholder for song
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            HttpResponseMessage response = await client.GetAsync($"api/Song/{id}"); // Send GET request for specific song

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); // Read response
                song = JsonConvert.DeserializeObject<Song>(responseContent); // Deserialize JSON
            }

            if (song == null)
            {
                return NotFound(); // Return 404 if song not found
            }

            return View(song); // Return view with song
        }
    }

    // POST: Song
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Song song)
    {
        if (song == null)
        {
            return BadRequest(); // Return 400 if input invalid
        }

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            var json = JsonConvert.SerializeObject(song); // Serialize song to JSON
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); // Create content for POST

            HttpResponseMessage response = await client.PostAsync("api/Song", content); // Send POST request

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect on success
            }
            return BadRequest(); // Return 400 if creation fails
        }
    }

    // PUT: Song/{songId}
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] Song song)
    {
        if (song == null || song.Id != id)
        {
            return BadRequest(); // Return 400 if input invalid
        }

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            var json = JsonConvert.SerializeObject(song); // Serialize song to JSON
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); // Create content for PUT

            HttpResponseMessage response = await client.PutAsync($"api/Song/{id}", content); // Send PUT request

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect on success
            }
            return BadRequest(); // Return 400 if update fails
        }
    }

    // DELETE: Song/{songId}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            HttpResponseMessage response = await client.DeleteAsync($"api/Song/{id}"); // Send DELETE request

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect on success
            }
            return NotFound(); // Return 404 if deletion fails
        }
    }

}
