using Feedback_System.Repository.Interface;
using Feedback_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository homeRepository;

        public HomeService(IHomeRepository _homeRepository)
        {
            homeRepository = _homeRepository;
        }
        public string GetOutput()
        {
            return homeRepository.GetOutputString() + ", From Service";
        }
    }
}
