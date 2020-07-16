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
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;
        private readonly EventMapper eventMapper;
        private readonly IMasterDataRepository masterRepository;
        private readonly IUserRepository userRepository;
        public EventService(IEventRepository _eventRepository, IMasterDataRepository _masterRepository, IUserRepository _userRepository)
        {
            eventRepository = _eventRepository;
            eventMapper = new EventMapper();
            masterRepository = _masterRepository;
            userRepository = _userRepository;
        }

        public List<EventsModel> GetEventsData()
        {
            List<Events> eventsEntityList = eventRepository.GetEventsData();
            return eventMapper.MapEventEntityListToModel(eventsEntityList);
        }

        public List<ParticipantsModel> GetParticipantsData()
        {
            List<Participants> partcipantsEntityList = eventRepository.GetParticipantsData();
            return eventMapper.MapParticipantEntityListToModel(partcipantsEntityList, userRepository);
        }

        public ParticipantsModel GetParticipantsDataById(int userId)
        {
            Participants partcipantsEntity = eventRepository.GetParticipantsDataById(userId);
            List<Participants> partcipantsEntityList = new List<Participants>();
            if (partcipantsEntity != null)
            {
                partcipantsEntityList.Add(partcipantsEntity);
            }
            return eventMapper.MapParticipantEntityListToModel(partcipantsEntityList, userRepository).ElementAtOrDefault(0);
        }

        public EventsModel GetEventDataById(string eventId)
        {
            Events eventEntity = eventRepository.GetEventDataById(eventId);
            List<Events> eventsEntityList = new List<Events>();
            if (eventEntity != null)
            {
                eventsEntityList.Add(eventEntity);
            }
            return eventMapper.MapEventEntityListToModel(eventsEntityList).ElementAtOrDefault(0);
        }

        public bool UpdatePartcipantData(ParticipantsModel participantModel)
        {
            Participants updatedParticipant = eventMapper.MapParticipantModeltoEntity(participantModel);
            eventRepository.UpdatePartcipantData(updatedParticipant);
            return true;
        }

        public List<ParticipantsModel> GetParticipantsDataByRoleUser(int roleId, int userId)
        {
            if (roleId == 1 || roleId == 2)
            {
                return GetParticipantsData();
            }
            else if (roleId == 3)
            {
                List<Participants> partEntityList = eventRepository.GetParticipantsData();
                List<Participants> pocEntityList = partEntityList.Where(x => x.AssociateId == userId && x.RoleId == roleId).ToList();
                List<Participants> returnParticipants = new List<Participants>();

                foreach (Participants pocEntity in pocEntityList)
                {
                    List<Participants> tempList = partEntityList.Where(x => x.EventId == pocEntity.EventId).ToList();
                    returnParticipants.AddRange(tempList);
                }

                return eventMapper.MapParticipantEntityListToModel(returnParticipants, userRepository);
            }
            else
            {
                return new List<ParticipantsModel>();
            }
        }

        public List<EventsModel> GetEventDataByRoleUser(int roleId, int userId)
        {
            if (roleId == 1 || roleId == 2)
            {
                return GetEventsData();
            }
            else if (roleId == 3)
            {
                List<Events> eventsEntityList = eventRepository.GetEventsData();
                List<Participants> partEntityList = eventRepository.GetParticipantsData();
                List<Participants> pocEntityList = partEntityList.Where(x => x.AssociateId == userId && x.RoleId == roleId).ToList();
                List<Events> returnEvents = new List<Events>();

                foreach (Participants pocEntity in pocEntityList)
                {
                    Events tempEvent = eventsEntityList.First(x => x.EventId == pocEntity.EventId);
                    returnEvents.Add(tempEvent);
                }

                return eventMapper.MapEventEntityListToModel(returnEvents);
            }
            else
            {
                return new List<EventsModel>();
            }
        }
    }



}
