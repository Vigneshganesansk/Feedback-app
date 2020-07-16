using Feedback_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Repository.Interface
{
    public interface IMasterDataRepository
    {
        List<RoleMaster> GetRoleMasterData();
        List<LocationMaster> GetLocationMasterData();
        List<Bumaster> GetBuMasterData();
        List<ParticipationStatusMaster> GetParticipationStatusMasterData();
    }
}
