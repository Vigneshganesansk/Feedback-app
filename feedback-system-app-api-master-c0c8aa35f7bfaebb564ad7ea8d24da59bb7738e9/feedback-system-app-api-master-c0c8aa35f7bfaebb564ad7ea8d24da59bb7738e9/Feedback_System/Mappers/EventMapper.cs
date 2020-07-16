using Feedback_System.Entities;
using Feedback_System.Models;
using Feedback_System.Repository.Interface;
using Feedback_System.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Mappers
{
    public class EventMapper
    {
        public List<EventsModel> MapEventEntityListToModel(List<Events> eventsEntityList)
        {
            // Logic
            List<EventsModel> eventsModelList = new List<EventsModel>();
            foreach (Events eventEntity in eventsEntityList)
            {
                EventsModel eventModel = new EventsModel();
                eventModel.EventID = eventEntity.EventId;
                eventModel.EventName = eventEntity.EventName;
                eventModel.EventDate = eventEntity.EventDate.ToString(Constants.DateFormat);
                eventModel.EventDescription = eventEntity.EventDescription;
                eventModel.EventStatus = eventEntity.EventStatus;
                eventModel.BaseLocation = eventEntity.Location;
                eventModel.CouncilName = eventEntity.CouncilName;
                eventModel.BeneficiaryName = eventEntity.BeneficiaryName;
                eventModel.Venue = eventEntity.Venue;
                eventModel.TotalVolenteers = eventEntity.TotalVolenteers;
                eventModel.TotalVolenteerHours = eventEntity.TotalVolenteerHours;
                eventModel.TotalTravelHours = eventEntity.TotalTravelHours;
                eventModel.OverallVolenteerHours = eventEntity.OverallVolenteerHours;
                eventModel.LivesImpacted = eventEntity.LivesImpacted;
                eventModel.Month = eventEntity.EventDate.ToString(Constants.MonthFormat);

                eventsModelList.Add(eventModel);
            }
            return eventsModelList;
        }
        public List<ParticipantsModel> MapParticipantEntityListToModel(List<Participants> participantssEntityList, IUserRepository userRepository)
        {
            List<Feedback> feedbackList = userRepository.GetFeedbackData();

            List<ParticipantsModel> participantModelList = new List<ParticipantsModel>();
            foreach (Participants participantEntity in participantssEntityList)
            {
                ParticipantsModel participantModel = new ParticipantsModel();
                participantModel.ParticipantGuid = participantEntity.Guid;
                participantModel.AssociateId = participantEntity.AssociateId;
                participantModel.AssociateName = participantEntity.AssociateName;
                participantModel.Email = participantEntity.Email;
                participantModel.EventId = participantEntity.EventId;
                participantModel.VolenteerHours = participantEntity.VolenteerHours;
                participantModel.TravelHours = participantEntity.TravelHours;
                participantModel.ParticipationStatusId = participantEntity.ParticipationStatusId;
                participantModel.ParticipationStatusName = Enum.GetName(typeof(CodeValues.ParticipationStatus), participantModel.ParticipationStatusId);
                participantModel.EventRating = participantEntity.EventRating;
                participantModel.RoleId = participantEntity.RoleId;
                participantModel.RoleName = Enum.GetName(typeof(CodeValues.UserRole), participantModel.RoleId);
                participantModel.IIEPCategory = participantEntity.Iiepcategory;

                Feedback likes = null;
                Feedback dislikes = null;
                if (participantModel.ParticipationStatusId == 1)
                {
                    likes = feedbackList?.Where(x => x.AssociateId == participantModel.AssociateId && x.EventId == participantModel.EventId && x.Question == Constants.LikeQuestion).FirstOrDefault();
                    dislikes = feedbackList?.Where(x => x.AssociateId == participantModel.AssociateId && x.EventId == participantModel.EventId && x.Question == Constants.DislikeQuestion).FirstOrDefault();
                }
                else if (participantModel.ParticipationStatusId == 2)
                {
                    likes = feedbackList?.Where(x => x.AssociateId == participantModel.AssociateId && x.EventId == participantModel.EventId && x.Question == Constants.NotParticipatedQuestion).FirstOrDefault();
                }
                else if (participantModel.ParticipationStatusId == 3)
                {
                    likes = feedbackList?.Where(x => x.AssociateId == participantModel.AssociateId && x.EventId == participantModel.EventId && x.Question == Constants.UnregisteredQuestion).FirstOrDefault();
                }
                participantModel.Likes = likes != null ? likes.Answer : "No Feedback";
                participantModel.Dislikes = dislikes != null ? dislikes.Answer : "No Feedback";
                participantModel.IsFeedbackSubmitted = participantEntity.IsFeedbackSubmitted == true;

                participantModelList.Add(participantModel);
            }
            return participantModelList;
        }

        public Participants MapParticipantModeltoEntity(ParticipantsModel participantModel)
        {
            Participants participantEntity = new Participants();
            participantEntity.Guid = participantModel.ParticipantGuid;
            participantEntity.AssociateId = participantModel.AssociateId;
            participantEntity.EventId = participantModel.EventId;
            participantEntity.VolenteerHours = participantModel.VolenteerHours;
            participantEntity.TravelHours = participantModel.TravelHours;
            participantEntity.ParticipationStatusId = participantModel.ParticipationStatusId;
            participantEntity.EventRating = participantModel.EventRating;
            participantEntity.RoleId = participantModel.RoleId;
            participantEntity.Iiepcategory = null;
            participantEntity.IsFeedbackSubmitted = participantModel.IsFeedbackSubmitted;
            return participantEntity;
        }
    }

}
