using Feedback_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Services.Interfaces
{
    public interface IMasterDataService
    {
        List<RoleMasterModel> GetRoleMasterData();
        List<BUMasterModel> GetBUMasterData();
        List<LocationMasterModel> GetLocationMasterData();
        List<ParticipantStatusMasterModel> GetParticipantsStatusMasterData();
    }
}
