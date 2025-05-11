using AppGestionStockMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AppGestionStockMVC.Services
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5048/api/Client/");

            // ⚠️ Remplace par ton vrai token ou méthode d'obtention
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJleHAiOjE3NDgxMDgxMjF9.MI-_pAVqxKeJfRrOGt51Jvp4d08hAJO2lx_9inHwzdM");
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            var response = await _httpClient.GetAsync("getClients");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Client>>(json);
            }

            // gestion d’erreur
            return new List<Client>();
        }

        public async Task<bool> AddClientAsync(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync("AddClient", client);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        // POST: Produits/Edit/5
        [HttpPost]
        public async Task<bool> EditClient(Client client)
        {

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonContent = JsonConvert.SerializeObject(client);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("EditClient", content);
            return response.IsSuccessStatusCode;
        }
    }
}
