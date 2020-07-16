using Feedback_System.Entities;
using Feedback_System.Mappers;
using Feedback_System.Models;
using Feedback_System.Repository.Interface;
using Feedback_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IMasterDataRepository masterDataRepository;
        private readonly MasterMapper masterMapper;
        public MasterDataService(IMasterDataRepository _masterDataRepository)
        {
            masterDataRepository = _masterDataRepository;
            masterMapper = new MasterMapper();
        }

        public List<RoleMasterModel> GetRoleMasterData()
        {
            List<RoleMaster> roleEntityList = masterDataRepository.GetRoleMasterData();
            return masterMapper.MapRoleMasterEntityToModel(roleEntityList);
        }

        public List<ParticipantStatusMasterModel> GetParticipantsStatusMasterData()
        {
            List<ParticipationStatusMaster> statusEntityList = masterDataRepository.GetParticipationStatusMasterData();
            return masterMapper.MapParticipantStatusMasterEntityToModel(statusEntityList);
        }

        public List<LocationMasterModel> GetLocationMasterData()
        {
            List<LocationMaster> locationEntityList = masterDataRepository.GetLocationMasterData();
            return masterMapper.MapLocationMasterEntityToModel(locationEntityList);
        }

        public List<BUMasterModel> GetBUMasterData()
        {
            List<Bumaster> buEntityList = masterDataRepository.GetBuMasterData();
            return masterMapper.MapBuMasterEntityToModel(buEntityList);
        }
    }
}
