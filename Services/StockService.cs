using System.Net.Http.Headers;
using System.Text;
using AppGestionStockMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppGestionStockMVC.Services
{
    public class StockService
    {
        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5048/api/Produit/");

            // ⚠️ Remplace par ton vrai token ou méthode d'obtention
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJleHAiOjE3NDgxMDgxMjF9.MI-_pAVqxKeJfRrOGt51Jvp4d08hAJO2lx_9inHwzdM");
        }

        public async Task<List<Produit>> GetProduitsAsync()
        {
            var response = await _httpClient.GetAsync("getProduits");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Produit>>(json);
            }

            // gestion d’erreur
            return new List<Produit>();
        }

        public async Task<bool> AddProduitAsync(Produit produit)
        {
            var response = await _httpClient.PostAsJsonAsync("AddProduct",produit);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduitAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        // POST: Produits/Edit/5
        [HttpPost]
        public async Task<bool> EditProduit(Produit produit)
        {
         
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(produit);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("EditProduit",content);
                return response.IsSuccessStatusCode;
         }
       }
    }










