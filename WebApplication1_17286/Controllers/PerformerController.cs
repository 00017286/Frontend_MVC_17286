using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1_17286.Models;

public class PerformerController : Controller
{
    private readonly string _baseUrl = "http://ec2-34-200-212-116.compute-1.amazonaws.com:44312/"; // Base URL for API

    // GET: Performer
    public async Task<IActionResult> Index()
    {
        List<Performer> performerInfo = new List<Performer>(); // List to store performers
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address for client
            client.DefaultRequestHeaders.Clear(); // Clear existing headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            HttpResponseMessage response = await client.GetAsync("api/Performer"); // Send GET request

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); // Read response as string
                performerInfo = JsonConvert.DeserializeObject<List<Performer>>(responseContent); // Deserialize JSON response
            }

            return View(performerInfo); // Return view with performers
        }
    }

    // GET: Performer/Create
    public IActionResult Create()
    {
        return View(); // Return view for creating a new performer
    }

    // POST: Performer/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Performer performer)
    {
        if (!ModelState.IsValid)
        {
            return View(performer); // Return the form with errors if model validation fails.
        }

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            var json = JsonConvert.SerializeObject(performer); // Serialize performer to JSON
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); // Create content for POST

            HttpResponseMessage response = await client.PostAsync("api/Performer", content); // Send POST request

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect on success
            }
            return View(performer); // Return view with errors if creation fails
        }
    }

    // GET: Performer/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        Performer performer = null; // Placeholder for performer
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            HttpResponseMessage response = await client.GetAsync($"api/Performer/{id}"); // Send GET request for specific performer

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); // Read response
                performer = JsonConvert.DeserializeObject<Performer>(responseContent); // Deserialize JSON
            }

            if (performer == null)
            {
                return NotFound(); // Return 404 if performer not found
            }

            return View(performer); // Return view with performer
        }
    }

    // POST: Performer/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Performer performer)
    {
        if (id != performer.Id || !ModelState.IsValid)
        {
            return View(performer); // Return the form with errors if model validation fails.
        }

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            var json = JsonConvert.SerializeObject(performer); // Serialize performer to JSON
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); // Create content for PUT

            HttpResponseMessage response = await client.PutAsync($"api/Performer/{id}", content); // Send PUT request

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect on success
            }
            return View(performer); // Return view with errors if update fails
        }
    }

    // GET: Performer/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        Performer performer = null; // Placeholder for performer
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            HttpResponseMessage response = await client.GetAsync($"api/Performer/{id}"); // Send GET request for specific performer

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); // Read response
                performer = JsonConvert.DeserializeObject<Performer>(responseContent); // Deserialize JSON
            }

            if (performer == null)
            {
                return NotFound(); // Return 404 if performer not found
            }

            return View(performer); // Return view with performer
        }
    }

    // POST: Performer/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl); // Set base address
            client.DefaultRequestHeaders.Clear(); // Clear headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Set content type

            HttpResponseMessage response = await client.DeleteAsync($"api/Performer/{id}"); // Send DELETE request

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect on success
            }
            return NotFound(); // Return 404 if deletion fails
        }
    }
}
