using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPA.Services
{
    interface ICPAClientWrapper
    {
        Task<List<ExamResultResponse>> GetResults();
    }
}
