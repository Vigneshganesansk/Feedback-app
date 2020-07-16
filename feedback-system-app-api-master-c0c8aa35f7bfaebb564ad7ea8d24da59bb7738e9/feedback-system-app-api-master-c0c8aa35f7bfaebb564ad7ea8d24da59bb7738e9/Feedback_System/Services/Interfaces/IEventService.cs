using Feedback_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Services.Interfaces
{

    public interface IEventService
    {
        List<EventsModel> GetEventsData();
        List<ParticipantsModel> GetParticipantsData();
        ParticipantsModel GetParticipantsDataById(int userId);
        EventsModel GetEventDataById(string eventId);
        bool UpdatePartcipantData(ParticipantsModel participantModel);
        List<ParticipantsModel> GetParticipantsDataByRoleUser(int roleId, int userId);
        List<EventsModel> GetEventDataByRoleUser(int roleId, int userId);
    }


}
