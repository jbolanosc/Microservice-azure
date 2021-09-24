using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Search.Interfaces;
using Microsoft.Extensions.Logging;

namespace MicroserviceCourse.Api.Search.Services
{
    public class CustomerService : ICustumerService
    {
        private readonly IHttpClientFactory _http;
        private readonly ILogger _logger;

        public CustomerService(IHttpClientFactory http, ILogger logger)
        {
            _http = http;
            _logger = logger;
        }
        public async Task<(bool isSuccess, dynamic Customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var client = _http.CreateClient("CustomerService");
                var response = await client.GetAsync($"api/customer/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, options);

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

        private object JsonSerializerOptions()
        {
            throw new NotImplementedException();
        }
    }
}
