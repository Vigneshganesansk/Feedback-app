using Feedback_System.Entities;
using Feedback_System.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Repository
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly FeedbackDbContext dbContext;

        public MasterDataRepository(FeedbackDbContext _context)
        {
            dbContext = _context;
        }

        public List<RoleMaster> GetRoleMasterData()
        {
            return dbContext.RoleMaster.ToList<RoleMaster>();
        }

        public List<LocationMaster> GetLocationMasterData()
        {
            return dbContext.LocationMaster.ToList<LocationMaster>();
        }

        public List<Bumaster> GetBuMasterData()
        {
            return dbContext.Bumaster.ToList<Bumaster>();
        }

        public List<ParticipationStatusMaster> GetParticipationStatusMasterData()
        {
            return dbContext.ParticipationStatusMaster.ToList<ParticipationStatusMaster>();
        }
    }
}
