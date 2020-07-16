using Feedback_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Repository.Interface
{
    public interface IEventRepository
    {
        List<Events> GetEventsData();
        List<Participants> GetParticipantsData();
        void PostParticipantData(List<Participants> participants);
        void PostEventsData(List<Events> events);
        Participants GetParticipantsDataById(int userId);
        Events GetEventDataById(string eventId);
        void UpdatePartcipantData(Participants updatedParticipant);
        void DeleteParticipantData(Guid participantGuid);
    }

}
