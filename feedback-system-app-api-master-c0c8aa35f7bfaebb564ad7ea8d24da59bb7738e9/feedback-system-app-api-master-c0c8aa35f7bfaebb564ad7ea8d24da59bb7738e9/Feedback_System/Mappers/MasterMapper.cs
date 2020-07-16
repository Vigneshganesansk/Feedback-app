using Feedback_System.Entities;
using Feedback_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Mappers
{
    public class MasterMapper
    {
        public List<RoleMasterModel> MapRoleMasterEntityToModel(List<RoleMaster> roleEntityList)
        {
            List<RoleMasterModel> roleModelList = new List<RoleMasterModel>();
            foreach (RoleMaster entity in roleEntityList)
            {
                RoleMasterModel model = new RoleMasterModel();
                model.RoleId = entity.RoleId;
                model.RoleName = entity.RoleName;
                roleModelList.Add(model);
            }
            return roleModelList;
        }

        public List<BUMasterModel> MapBuMasterEntityToModel(List<Bumaster> buEntityList)
        {
            List<BUMasterModel> buModelList = new List<BUMasterModel>();
            foreach (Bumaster entity in buEntityList)
            {
                BUMasterModel model = new BUMasterModel();
                model.Buid = entity.Buid;
                model.Buname = entity.Buname;
                model.BuheadId = entity.BuheadId;
                buModelList.Add(model);
            }
            return buModelList;
        }
        public List<LocationMasterModel> MapLocationMasterEntityToModel(List<LocationMaster> locationEntityList)
        {
            List<LocationMasterModel> locationModelList = new List<LocationMasterModel>();
            foreach (LocationMaster entity in locationEntityList)
            {
                LocationMasterModel model = new LocationMasterModel();
                model.LocationId = entity.LocationId;
                model.LocationName = entity.LocationName;
                locationModelList.Add(model);
            }
            return locationModelList;
        }
        public List<ParticipantStatusMasterModel> MapParticipantStatusMasterEntityToModel(List<ParticipationStatusMaster> statusEntityList)
        {
            List<ParticipantStatusMasterModel> statusModelList = new List<ParticipantStatusMasterModel>();
            foreach (ParticipationStatusMaster entity in statusEntityList)
            {
                ParticipantStatusMasterModel model = new ParticipantStatusMasterModel();
                model.StatusId = entity.StatusId;
                model.StatusName = entity.StatusName;
                statusModelList.Add(model);
            }
            return statusModelList;
        }
    }
}
