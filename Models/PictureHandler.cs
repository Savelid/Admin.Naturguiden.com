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
    public class PictureHandler
    {
        static HttpClient client = new HttpClient();
        public static void Setup()
        {
            if(client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("http://api.naturguiden.com/");
                //client.BaseAddress = new Uri("http://localhost:5076/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        internal static string SaveImage(WebImage webImage)
        {
            var newFileName = "";
            var imagePath = "";

            if (webImage != null)
            {
                newFileName = Guid.NewGuid().ToString() + "_" +
                Path.GetFileName(webImage.FileName);
                imagePath = @"images/" + newFileName;

                webImage.Save(@"~/" + imagePath);

                return imagePath;
            }
            else
            {
                return "Image is null";
            }
        }

        public static async Task<libraryNaturguiden.Picture[]> GetPicturesAsync()
        {
            Setup();
            libraryNaturguiden.Picture[] product = null;
            HttpResponseMessage response = await client.GetAsync("api/Picture");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<libraryNaturguiden.Picture[]>();
            }
            return product;
        }

        public static async Task<libraryNaturguiden.Picture> GetPictureAsync(int id)
        {
            Setup();
            libraryNaturguiden.Picture product = null;
            HttpResponseMessage response = await client.GetAsync("api/Picture/" + id);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<libraryNaturguiden.Picture>();
            }
            return product;
        }

        public static async Task<HttpStatusCode> CreatePictureAsync(libraryNaturguiden.Picture picture)
        {
            FormatPicture(picture);

            Setup();
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Picture", picture);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public static async Task<HttpStatusCode> ReformatPictureAsync(libraryNaturguiden.Picture picture)
        {
            FormatPicture(picture);

            Setup();
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Picture/{picture.Id}", picture);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        private static void FormatPicture(Picture picture)
        {
            WebImage img = new WebImage(@"C:/Users/Administrator/Documents/Webprojects/adminNaturguiden/" + picture.Url);
            var imgThumb = img.Clone();
            var imgFormated = img.Clone();
            imgThumb.Resize(606, 406, false, false);
            imgThumb.Crop(3, 3, 3, 3);
            imgFormated.Resize(1806, 1206, true, true);
            imgFormated.Crop(3, 3, 3, 3);

            var filename = picture.Url.Split('/');
            picture.FileName = filename.LastOrDefault();

            picture.ThumbUrl = "images/album/thumbs/" + picture.FileName;
            picture.FormatedUrl = "images/album/" + picture.FileName;
            imgThumb.Save("../" + picture.ThumbUrl);
            imgFormated.Save("../" + picture.FormatedUrl);

            picture.FileName = picture.FileName.Substring(37);
        }

        public static async Task<HttpStatusCode> UpdatePictureAsync(libraryNaturguiden.Picture picture)
        {
            Setup();
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Picture/{picture.Id}", picture);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public static async Task<HttpStatusCode> DeletePictureAsync(int id)
        {
            Setup();
            HttpResponseMessage response = await client.DeleteAsync($"api/Picture/{id}");
            return response.StatusCode;
        }

        public static async Task<libraryNaturguiden.PictureCategory[]> GetCategoriesAsync()
        {
            Setup();
            libraryNaturguiden.PictureCategory[] category = null;
            HttpResponseMessage response = await client.GetAsync("api/PictureCategory");
            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<libraryNaturguiden.PictureCategory[]>();
            }
            return category;
        }

        public static async Task<libraryNaturguiden.PictureCategory> GetCategoryAsync(int id)
        {
            Setup();
            libraryNaturguiden.PictureCategory category = null;
            HttpResponseMessage response = await client.GetAsync($"api/PictureCategory/{id}");
            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<libraryNaturguiden.PictureCategory>();
            }
            return category;
        }

        public static async Task<HttpStatusCode> CreateCategoryAsync(libraryNaturguiden.PictureCategory category)
        {
            Setup();
            HttpResponseMessage response = await client.PostAsJsonAsync("api/PictureCategory", category);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public static async Task<HttpStatusCode> UpdateCategoryAsync(libraryNaturguiden.PictureCategory category)
        {
            Setup();
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/PictureCategory/{category.Id}", category);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public static async Task<HttpStatusCode> DeleteCategoryAsync(int id)
        {
            Setup();
            HttpResponseMessage response = await client.DeleteAsync($"api/PictureCategory/{id}");
            return response.StatusCode;
        }
    }
}