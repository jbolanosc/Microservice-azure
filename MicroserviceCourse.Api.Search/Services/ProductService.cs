using MicroserviceCourse.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace MicroserviceCourse.Api.Search.Services
{
    public class ProductService : Interfaces.IProductService
    {
        private readonly IHttpClientFactory _http;
        private readonly Logger<Interfaces.IProductService> _logger;
        private readonly Interfaces.IProductService _service;

        public ProductService(Interfaces.IProductService service, IHttpClientFactory http, Logger<Interfaces.IProductService> logger)
        {
            _http = http;
            _logger = logger;
            _service = service;
        }
        public async Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var client = _http.CreateClient("ProductService");
                var response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
