using Feedback_System.Entities;
using Feedback_System.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly FeedbackDbContext dbContext;

        public EventRepository(FeedbackDbContext _context)
        {
            dbContext = _context;
        }

        public List<Events> GetEventsData()
        {
            return dbContext.Events.ToList<Events>();
        }

        public void PostEventsData(List<Events> events)
        {
            dbContext.Events.AddRange(events);
            dbContext.SaveChanges();
        }

        public List<Participants> GetParticipantsData()
        {
            return dbContext.Participants.ToList<Participants>();
        }

        public void PostParticipantData(List<Participants> participants)
        {
            dbContext.Participants.AddRange(participants);
            dbContext.SaveChanges();
        }

        public Participants GetParticipantsDataById(int userId)
        {
            return dbContext.Participants.FirstOrDefault<Participants>(x => x.AssociateId == userId);
        }

        public Events GetEventDataById(string eventId)
        {
            return dbContext.Events.First<Events>(x => x.EventId == eventId);
        }

        public void UpdatePartcipantData(Participants updatedParticipant)
        {
            this.DeleteParticipantData(updatedParticipant.Guid);
            List<Participants> pList = new List<Participants>();
            pList.Add(updatedParticipant);
            this.PostParticipantData(pList);
        }

        public void DeleteParticipantData(Guid participantGuid)
        {
            Participants existingEntity = dbContext.Participants.FirstOrDefault(x => x.Guid == participantGuid);
            dbContext.Participants.Remove(existingEntity);

            dbContext.SaveChanges();
        }
    }

}
