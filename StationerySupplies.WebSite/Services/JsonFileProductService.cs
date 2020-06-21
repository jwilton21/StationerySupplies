using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using StationerySupplies.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileProductService
    {   // Constructor
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        // Retrieve Json file so the webhost enviornment knows the path where file comes from.
        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {   // Return from path. Combine 3 parts.
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        
       public IEnumerable<Product> GetProducts()
        {   // Convert text from json into product.
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {   // Array of products.
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }
    }

}
