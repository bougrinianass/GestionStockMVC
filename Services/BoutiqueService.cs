using System.Net.Http.Headers;
using AppGestionStockMVC.Models;
using Newtonsoft.Json;

namespace AppGestionStockMVC.Services
{
    public class BoutiqueService
    {
        private readonly HttpClient _httpClient;
        
        public BoutiqueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5048/api/Commande/");
        }
        public async Task<List<Commande>> GetCommandesAsync()
        {
            var response = await _httpClient.GetAsync("getCommandes");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Commande>>(json);
            }

            // gestion d’erreur
            return new List<Commande>();
        }

        public async Task<bool> AddCommandeAsync(Commande commande)
        {
            var response = await _httpClient.PostAsJsonAsync("AddCommande", commande);

            return response.IsSuccessStatusCode;
        }


    }
}
