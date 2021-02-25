using Newtonsoft.Json;
using System.Collections.Generic;

namespace CPA.Services
{
    public class ExamResultResponse
    {
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<Result> Results { get; set; }


    }

    public class Result
    {
        [JsonProperty(PropertyName = "year")]
        public string Year { get; set; }

        [JsonProperty(PropertyName = "grade")]
        public string Grade { get; set; }
    }   


}