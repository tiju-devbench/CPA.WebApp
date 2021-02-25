using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;

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

        public async Task<List<PassedSubjects>> LoadPassedSubjects()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("/api/results");
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ExamResultResponse>>(apiResponse);
            var passedsubjects = GetPassedSubjects(result);
            return passedsubjects;
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
        private List<PassedSubjects> GetPassedSubjects(List<ExamResultResponse> result)
        {
            var res = result.SelectMany(r => r.Results.Where(x => x.Grade == "PASS"), (a, b) => (b.Year, a.Subject))
                .GroupBy(x => x.Year)
                .Select(group =>
                        new PassedSubjects
                        {
                            Year = group.Key,
                            Subjects = group.OrderBy(x => x.Subject).Select(x => x.Subject).ToList()
                        }).OrderBy(x => x.Year);


            return res.ToList();
        }
    }
}
