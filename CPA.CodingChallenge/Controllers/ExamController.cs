using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPA.Services;
using Microsoft.Extensions.Configuration;

namespace CPA.CodingChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        

        private readonly ILogger<ExamController> _logger;
        private readonly IConfiguration _config;

        public ExamController(ILogger<ExamController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> Results()
        {
            try
            {
                CPAClientWrapper client = new CPAClientWrapper(_config);
                var result  = await client.GetResults();
                return Ok(result);
            }
            catch(Exception ex)
            {
                var exceptionMessage = "Error occured in getting Examresults from CPA" + ex.Message;
                return BadRequest(exceptionMessage);
            }
           return BadRequest();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> LoadPassedSubjects()
        {
            try
            {
                CPAClientWrapper client = new CPAClientWrapper(_config);
                var result = await client.GetResults();
                var passedsubjects = GetPassedSubjects(result);

                return Ok(passedsubjects);
            }
            catch (Exception ex)
            {
                var exceptionMessage = "Error occured in getting Examresults from CPA" + ex.Message;
                return BadRequest(exceptionMessage);
            }
            return BadRequest();
        }

        private List<PassedSubjects> GetPassedSubjects(List<ExamResultResponse> result)
        {
            List<ExamResultResponse> filteredList = new List<ExamResultResponse>();
            PassedSubjects sub = new PassedSubjects();



            var res = result.SelectMany(r => r.Results.Where(x => x.Grade == "PASS"), (a, b) => (b.Year, a.Subject))
                .GroupBy(x => x.Year)
                .Select(group =>
                        new PassedSubjects
                        {
                            Year = group.Key,
                            Subjects = group.OrderBy(x => x.Subject).Select(x=> x.Subject).ToList()
                        }).OrderBy(x=> x.Year);
                

            return res.ToList();
        }
    }
}
