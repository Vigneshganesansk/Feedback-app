using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Models
{
    public class MasterDataModel
    {
        public RoleMasterModel[] roleMasterModel { get; set; }
        public BUMasterModel[] buMasterModel { get; set; }
        public LocationMasterModel[] locationMasterModel { get; set; }
        public ParticipantStatusMasterModel[] pptStatusMasterModel { get; set; }
    }
}
