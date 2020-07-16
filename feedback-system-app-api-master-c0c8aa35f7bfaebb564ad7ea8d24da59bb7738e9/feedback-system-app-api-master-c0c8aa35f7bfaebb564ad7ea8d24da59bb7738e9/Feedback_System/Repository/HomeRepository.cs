using Feedback_System.Entities;
using Feedback_System.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly FeedbackDbContext dbContext;

        public HomeRepository(FeedbackDbContext _context)
        {
            dbContext = _context;
        }
        public string GetOutputString()
        {
            //return "From Repo";
            //return dbContext.Student.FirstOrDefault(x => x.Id == 123).Name;
            return dbContext.Events.FirstOrDefault(x => x.EventId == "EVNT00047261").EventName;
        }
    }
}
