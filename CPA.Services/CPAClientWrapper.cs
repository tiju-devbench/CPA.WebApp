using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CPA.Services
{
    public class CPAClientWrapper : ICPAClientWrapper
    {
        private readonly IConfiguration _config;

        public CPAClientWrapper(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<ExamResultResponse>> GetResults()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("/api/results");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ExamResultResponse>>(apiResponse);
        }


        private HttpClient GetHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(_config.GetSection("CPAExamResultsBaseUrl").Value);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
