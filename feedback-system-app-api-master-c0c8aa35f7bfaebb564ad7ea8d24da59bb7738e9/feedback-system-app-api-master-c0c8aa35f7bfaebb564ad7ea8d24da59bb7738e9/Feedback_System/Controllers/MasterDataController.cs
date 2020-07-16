using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feedback_System.Models;
using Feedback_System.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback_System.Controllers
{
    [Route("api/master")]
    [ApiController]
    public class MasterDataController
    {
        private readonly IMasterDataService masterDataService;
        public MasterDataController(IMasterDataService _masterDataService)
        {
            masterDataService = _masterDataService;
        }

        [HttpGet]
        [Route("getRoleMasterData")]
        public ActionResult<List<RoleMasterModel>> GetRoleMasterData()
        {
            return masterDataService.GetRoleMasterData();
        }

        [HttpGet]
        [Route("getBUMasterData")]
        public ActionResult<List<BUMasterModel>> GetBUMasterData()
        {
            return masterDataService.GetBUMasterData();
        }

        [HttpGet]
        [Route("getLocationMasterData")]
        public ActionResult<List<LocationMasterModel>> GetLocationMasterData()
        {
            return masterDataService.GetLocationMasterData();
        }

        [HttpGet]
        [Route("getParticipantsStatusMasterData")]
        public ActionResult<List<ParticipantStatusMasterModel>> GetParticipantsStatusMasterData()
        {
            return masterDataService.GetParticipantsStatusMasterData();
        }

    }


}