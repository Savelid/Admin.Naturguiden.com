using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using libraryNaturguiden;
using System.IO;

namespace adminNaturguiden.Models
{
    public class NewsHandler
    {
        static HttpClient client = new HttpClient();
        public static void Setup()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("http://api.naturguiden.com/");
                //client.BaseAddress = new Uri("http://localhost:5076/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public static async Task<libraryNaturguiden.News[]> GetNewsAsync()
        {
            Setup();
            libraryNaturguiden.News[] news = null;
            HttpResponseMessage response = await client.GetAsync("api/News");
            if (response.IsSuccessStatusCode)
            {
                news = await response.Content.ReadAsAsync<libraryNaturguiden.News[]>();
            }
            return news;
        }

        public static async Task<libraryNaturguiden.News> GetNewsAsync(int id)
        {
            Setup();
            libraryNaturguiden.News news = null;
            HttpResponseMessage response = await client.GetAsync("api/News/" + id);
            if (response.IsSuccessStatusCode)
            {
                news = await response.Content.ReadAsAsync<libraryNaturguiden.News>();
            }
            return news;
        }

        public static async Task<HttpStatusCode> CreateNewsAsync(libraryNaturguiden.News news)
        {
            Setup();
            HttpResponseMessage response = await client.PostAsJsonAsync("api/News", news);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public static async Task<HttpStatusCode> UpdateNewsAsync(libraryNaturguiden.News news)
        {
            Setup();
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/News/{news.Id}", news);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public static async Task<HttpStatusCode> DeleteNewsAsync(int id)
        {
            Setup();
            HttpResponseMessage response = await client.DeleteAsync($"api/News/{id}");
            return response.StatusCode;
        }
    }
}