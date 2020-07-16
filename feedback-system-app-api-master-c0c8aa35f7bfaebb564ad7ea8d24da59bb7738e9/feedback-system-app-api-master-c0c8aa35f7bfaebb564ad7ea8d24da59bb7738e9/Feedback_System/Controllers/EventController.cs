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

    [EnableCors("AllowAll")]
    [Route("api/event")]
    [ApiController]
    public class EventController
    {
        private readonly IEventService eventService;
        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        [HttpGet]
        [Route("getEventsData")]
        public ActionResult<List<EventsModel>> GetEventsData()
        {
            return eventService.GetEventsData();
        }

        [HttpGet]
        [Route("getParticipantsData")]
        public ActionResult<List<ParticipantsModel>> GetParticipantsData()
        {
            return eventService.GetParticipantsData();
        }

        [HttpGet]
        [Route("getParticipantsDataById/{userId}")]
        public ActionResult<ParticipantsModel> GetParticipantsDataById(int userId = 0)
        {
            return eventService.GetParticipantsDataById(userId);
        }

        [HttpGet]
        [Route("getParticipantsDataByRoleUser/{roleId}/{userId}")]
        public ActionResult<List<ParticipantsModel>> GetParticipantsDataByRoleUser(int roleId, int userId = 0)
        {
            return eventService.GetParticipantsDataByRoleUser(roleId, userId);
        }

        [HttpGet]
        [Route("getEventDataById/{eventId}")]
        public ActionResult<EventsModel> GetEventDataById(string eventId = "")
        {
            return eventService.GetEventDataById(eventId);
        }

        [HttpGet]
        [Route("getEventDataByRoleUser/{roleId}/{userId}")]
        public ActionResult<List<EventsModel>> GetEventDataByRoleUser(int roleId, int userId = 0)
        {
            return eventService.GetEventDataByRoleUser(roleId, userId);
        }

        [HttpPost]
        [Route("updateParticipantsData")]
        public ActionResult<bool> UpdateParticipantsData([FromBody] ParticipantsModel updatedParticipant)
        {
            return eventService.UpdatePartcipantData(updatedParticipant);
        }
    }

}