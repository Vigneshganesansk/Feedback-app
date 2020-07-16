using Feedback_System.Entities;
using Feedback_System.Mappers;
using Feedback_System.Repository.Interface;
using Feedback_System.Services.Interfaces;
using Feedback_System.Utilities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Services
{
    public class ExcelUploadService : IExcelUploadService
    {
        private readonly IEventRepository eventRepository;
        private readonly IMasterDataRepository masterRepository;
        private readonly IUserRepository userRepository;
        public ExcelUploadService(IEventRepository _eventRepository, IMasterDataRepository _masterRepository, IUserRepository _userRepository)
        {
            eventRepository = _eventRepository;
            masterRepository = _masterRepository;
            userRepository = _userRepository;
        }

        public void ReadExcel(string FilePath)
        {
            FileInfo existingFile = new FileInfo(FilePath);
            if (!existingFile.Exists)
                return;

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                List<Participants> participantsList = new List<Participants>();
                List<EmployeeUserMaster> userMasters = new List<EmployeeUserMaster>();
                List<Events> eventsList = new List<Events>();
                List<LocationMaster> locList = masterRepository.GetLocationMasterData();

                List<EmployeeUserMaster> existingUsers = userRepository.GetUserData();
                List<Participants> existingParticipants = eventRepository.GetParticipantsData();
                List<Events> existingEvents = eventRepository.GetEventsData();

                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    Participants pModel = new Participants();
                    pModel.Guid = Guid.NewGuid();
                    pModel.AssociateId = Convert.ToInt32(worksheet.Cells[row, 8].Value?.ToString().Trim());
                    pModel.EventId = worksheet.Cells[row, 1].Value?.ToString().Trim();
                    if (existingParticipants.Find(x => x.AssociateId == pModel.AssociateId && x.EventId == pModel.EventId) == null)
                    {
                        //pModel.AssociateName = Convert.ToString(worksheet.Cells[row, 9].Value?.ToString().Trim());
                        //pModel.Email = pModel.AssociateId.ToString() + "@cognizant.com";
                        pModel.ParticipationStatusId = 1;
                        pModel.VolenteerHours = Convert.ToDecimal(worksheet.Cells[row, 10].Value?.ToString().Trim());
                        pModel.TravelHours = Convert.ToDecimal(worksheet.Cells[row, 11].Value?.ToString().Trim());
                        pModel.RoleId = 4;
                        pModel.Iiepcategory = Convert.ToString(worksheet.Cells[row, 15].Value?.ToString().Trim());
                        participantsList.Add(pModel);
                    }
                }

                ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                int colCount2 = worksheet2.Dimension.End.Column;  //get Column Count
                int rowCount2 = worksheet2.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount2; row++)
                {
                    Events eventModel = new Events();
                    eventModel.EventId = worksheet2.Cells[row, 1].Value?.ToString().Trim();

                    if (existingEvents.Find(x => x.EventId == eventModel.EventId) == null)
                    {
                        eventModel.EventName = worksheet2.Cells[row, 9].Value?.ToString().Trim();
                        eventModel.EventDescription = worksheet2.Cells[row, 10].Value?.ToString().Trim();
                        eventModel.EventDate = Convert.ToDateTime(worksheet2.Cells[row, 11].Value?.ToString().Trim());
                        eventModel.EventStatus = worksheet2.Cells[row, 18].Value?.ToString().Trim();
                        eventModel.CouncilName = worksheet2.Cells[row, 6].Value?.ToString().Trim();
                        eventModel.TotalVolenteers = Convert.ToInt32(worksheet2.Cells[row, 12].Value?.ToString().Trim());
                        eventModel.TotalVolenteerHours = Convert.ToDecimal(worksheet2.Cells[row, 13].Value?.ToString().Trim());
                        eventModel.TotalTravelHours = Convert.ToDecimal(worksheet2.Cells[row, 14].Value?.ToString().Trim());
                        eventModel.OverallVolenteerHours = Convert.ToDecimal(worksheet2.Cells[row, 15].Value?.ToString().Trim());
                        eventModel.LivesImpacted = Convert.ToInt32(worksheet2.Cells[row, 16].Value?.ToString().Trim());
                        eventModel.BeneficiaryName = worksheet2.Cells[row, 4].Value?.ToString().Trim();
                        eventModel.Venue = worksheet2.Cells[row, 5].Value?.ToString().Trim();
                        eventModel.AddedUserId = 111111;
                        eventModel.AddedDate = DateTime.Now;
                        eventModel.IsDeleted = false;
                        eventModel.Location = worksheet2.Cells[row, 3].Value?.ToString().Trim().ToLower();
                        eventsList.Add(eventModel);

                        // Process POC Info and add it in Participants table with roleid = 2 and event id
                        string[] pocIDs = (worksheet2.Cells[row, 19].Value?.ToString().Trim()).Split(';');
                        for (int i = 0; i < pocIDs.Length; i++)
                        {
                            var particiapnt = participantsList.FirstOrDefault(x => x.AssociateId == Convert.ToInt32(pocIDs[i]) && x.EventId == eventModel.EventId);
                            if (particiapnt != null)
                            {
                                if (existingUsers.Find(x => x.AssociateId == particiapnt.AssociateId) == null)
                                {
                                    EmployeeUserMaster userEntity = new EmployeeUserMaster();
                                    Encryptor encryptorObj = new Encryptor();

                                    userEntity.AssociateId = particiapnt.AssociateId;
                                    userEntity.AssociateName = particiapnt.AssociateName.ToString().Trim();
                                    userEntity.AssociateName = "Associate Name";
                                    userEntity.Email = userEntity.AssociateId.ToString() + "@cognizant.com";
                                    userEntity.Salt = encryptorObj.getSalt();
                                    userEntity.Password = encryptorObj.getHash(Constants.DefaultPass + userEntity.Salt);
                                    userEntity.Designation = "Associate";
                                    userEntity.IsDeleted = false;
                                    userEntity.DeletedDate = null;
                                    userEntity.RoleId = 3;
                                    userMasters.Add(userEntity);
                                }
                                particiapnt.RoleId = 3;
                            }
                        }
                    }
                }

                ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                int colCount3 = worksheet3.Dimension.End.Column;  //get Column Count
                int rowCount3 = worksheet3.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount3; row++)
                {
                    Participants pModel = new Participants();
                    pModel.Guid = Guid.NewGuid();
                    pModel.AssociateId = Convert.ToInt32(worksheet3.Cells[row, 6].Value?.ToString().Trim());
                    pModel.EventId = worksheet3.Cells[row, 1].Value?.ToString().Trim();
                    if (existingParticipants.Find(x => x.AssociateId == pModel.AssociateId && x.EventId == pModel.EventId) == null)
                    {
                        pModel.ParticipationStatusId = 2;
                        pModel.VolenteerHours = 0;
                        pModel.TravelHours = 0;
                        pModel.RoleId = 3;
                        pModel.Iiepcategory = "...";
                        participantsList.Add(pModel);
                    }
                }

                ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                int colCount4 = worksheet4.Dimension.End.Column;  //get Column Count
                int rowCount4 = worksheet4.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount4; row++)
                {
                    Participants pModel = new Participants();
                    pModel.Guid = Guid.NewGuid();
                    pModel.AssociateId = Convert.ToInt32(worksheet4.Cells[row, 6].Value?.ToString().Trim());
                    pModel.EventId = worksheet4.Cells[row, 1].Value?.ToString().Trim();
                    if (existingParticipants.Find(x => x.AssociateId == pModel.AssociateId && x.EventId == pModel.EventId) == null)
                    {
                        pModel.ParticipationStatusId = 3;
                        pModel.VolenteerHours = 0;
                        pModel.TravelHours = 0;
                        pModel.RoleId = 3;
                        pModel.Iiepcategory = "...";
                        participantsList.Add(pModel);
                    }
                }

                userRepository.PostUserData(userMasters);
                eventRepository.PostEventsData(eventsList);
                eventRepository.PostParticipantData(participantsList);

            }
        }
    }


}
