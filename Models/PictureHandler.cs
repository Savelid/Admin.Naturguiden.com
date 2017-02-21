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

        public static async Task<libraryNaturguiden.Picture[]> GetPicturesAsync()
        {
            Setup();
            libraryNaturguiden.Picture[] value = null;
            HttpResponseMessage response = await client.GetAsync("api/Picture");
            if (response.IsSuccessStatusCode)
            {
                value = await response.Content.ReadAsAsync<libraryNaturguiden.Picture[]>();
            }
            return value;
        }

        public static async Task<libraryNaturguiden.Picture> GetPictureAsync(int id)
        {
            Setup();
            libraryNaturguiden.Picture value = null;
            HttpResponseMessage response = await client.GetAsync("api/Picture/" + id);
            if (response.IsSuccessStatusCode)
            {
                value = await response.Content.ReadAsAsync<libraryNaturguiden.Picture>();
            }
            return value;
        }

        public static async Task<HttpStatusCode> CreatePictureAsync(libraryNaturguiden.Picture picture, WebImage img)
        {
            if (picture.Format.ToLower().Equals("album"))
            {
                FormatPictureForAlbum(img, picture);
            }
            else if (picture.Format.ToLower().Equals("news"))
            {
                FormatPictureForNews(img, picture);
            }
            picture.Url = $"images/{picture.FileName}_orginal_{Guid.NewGuid()}.{img.ImageFormat}";
            img.Save("../" + picture.Url);

            Setup();
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Picture", picture);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        private static void FormatPictureForAlbum(WebImage img, libraryNaturguiden.Picture picture)
        {
            var imgThumb = img.Clone();
            var imgFormated = img.Clone();
            imgThumb = cropImage(imgThumb, 600, 400);
            imgFormated.Resize(1806, 1206, true, true);
            imgFormated.Crop(3, 3, 3, 3);

            picture.ThumbUrl = $"images/album/thumbs/{picture.FileName}_thumb_{Guid.NewGuid()}.{img.ImageFormat}";
            picture.FormatedUrl = $"images/album/{picture.FileName}_album_{Guid.NewGuid()}.{img.ImageFormat}";
            imgThumb.Save("../" + picture.ThumbUrl);
            imgFormated.Save("../" + picture.FormatedUrl);
        }

        private static void FormatPictureForNews(WebImage img, libraryNaturguiden.Picture picture)
        {
            var imgFormated = img.Clone();
            imgFormated = cropImage(imgFormated, 400, 200);

            picture.FormatedUrl = $"images/news/{picture.FileName} _news_{Guid.NewGuid()}.{img.ImageFormat}";
            imgFormated.Save("../" + picture.FormatedUrl);
        }
        private static WebImage cropImage(WebImage img, int width, int height)
        {
            double x = (double)img.Width / (double)width;
            double y = (double)img.Height / (double)height;
            int resizeLimitX = x > y ? img.Width : width + 6;
            int resizeLimitY = y > x ? img.Height : height + 6;
            img.Resize(resizeLimitX, resizeLimitY, true, false);
            int toCropX = (img.Width - width) / 2;
            int toCropY = (img.Height - height) / 2;
            img.Crop(toCropY, toCropX, toCropY, toCropX);
            return img;
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