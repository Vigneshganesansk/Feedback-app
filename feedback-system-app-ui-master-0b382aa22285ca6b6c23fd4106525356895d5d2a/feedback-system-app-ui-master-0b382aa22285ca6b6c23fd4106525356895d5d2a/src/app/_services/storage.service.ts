import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Report } from '../_models/report';
import { Questions } from '../_models/questions';
import { Observable, BehaviorSubject, throwError  } from 'rxjs';
import { AnswersFormValue } from '../_models/AnswersFormValue';
import { UserModel } from '../_models/userModel';
import { Events } from '../_models/events';
import { POCModel } from '../_models/pocModel';
import { Feedback } from '../_models/feedback';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor( private http:HttpClient ) { }

  events: Events[];
  particpants: any[];
  areReportsFetched: boolean = false;

  private userRole = new BehaviorSubject<number>(-1);
  private reportList = new BehaviorSubject<Report[]>([]);

  userdataURL: string = environment.userdataURL;
  questionsGetURL: string = environment.questionsGetURL;
  questionsPostURL: string = environment.questionsPostURL;
  questionsUpdateURL: string = environment.questionsUpdateURL;
  questionsDeleteURL: string = environment.questionsDeleteURL;
  eventGetUrl: string = environment.eventGetUrl;
  participantGetUrl: string = environment.participantGetUrl;
  participantUpdateUrl: string = environment.participantUpdateUrl;
  buMasterUrl: string = environment.buMasterUrl;
  getParticipantByIdUrl: string = environment.getParticipantByIdUrl;
  getEventbyIdUrl: string = environment.getEventbyIdUrl;
  feedbackPostURL: string = environment.feedbackPostURL;
  excelUploadURL: string = environment.excelUploadURL;
  getParticipantByUserRoleUrl: string = environment.getParticipantByUserRoleUrl;
  getEventbyUserRoleUrl: string = environment.getEventbyUserRoleUrl;

  postExcelData(file: File): Observable<boolean> {
    const formData = new FormData();
    formData.append('file',file[0],file[0].name);
    return this.http.post<boolean>(this.excelUploadURL, formData);
  }

  getQuestionsData(): Observable<Questions[]>{
    return  this.http.get<Questions[]>(this.questionsGetURL);
  }

  postQuestions(answers: AnswersFormValue): Observable<Questions[]>{
    return this.http.post<Questions[]>(this.questionsPostURL, answers);
  }

  postFeedback(feedbackList: Feedback[]): Observable<boolean> {
    return this.http.post<boolean>(this.feedbackPostURL, feedbackList);
  }

  updateQuestions(questions: Questions,index: number):Observable<Questions[]>{
    // this.questionsUpdateURL += index;
    return this.http.post<Questions[]>(`${this.questionsUpdateURL}/${index}`, questions);
  }

  updateParticipantData(participant: any): Observable<boolean> {
    return this.http.post<boolean>(this.participantUpdateUrl, participant);
  }

  deleteQuestions(index: number):Observable<Questions[]>{
    return this.http.post<Questions[]>(this.questionsDeleteURL, index);
  }

  getAllGuys():Observable<UserModel[]>{
    return this.http.get<UserModel[]>(this.userdataURL);
  }

  getReportsData(): Report[] {
    return [];
  }

  getEventData(): Promise<Events[]> {
    return  this.http.get<Events[]>(this.eventGetUrl).toPromise();
  }

  getEventDataByRoleIdUserId(userId: number, roleId: number): Promise<Events[]> {
    return this.http.get<Events[]>(`${this.getEventbyUserRoleUrl}/${userId}/${roleId}`).toPromise();
  }

  getParticiapantDataByRoleIdUserId(userId: number, roleId: number): Promise<any[]> {
    return this.http.get<any[]>(`${this.getParticipantByUserRoleUrl}/${userId}/${roleId}`).toPromise();
  }

  getParticiapantData(): Promise<any[]> {
    return  this.http.get<any[]>(this.participantGetUrl).toPromise();
  }

  getParticipantById(userId: number): Promise<any> {
    return  this.http.get<any>(`${this.getParticipantByIdUrl}/${userId}`).toPromise();
  }

  getEventById(eventId: string): Promise<any> {
    return  this.http.get<any>(`${this.getEventbyIdUrl}/${eventId}`).toPromise();
  }
  
  get getReportData() {
    return this.reportList.asObservable();
  }

  setEventReportData(roleId: number, userId: number) { // based on user and event later.
    let reportData: Report[] = [];
    // let user = this.authService.currentUserValue;
    this.getEventDataByRoleIdUserId(roleId, userId).then( response1 => {
      this.events = response1;
      this.getParticiapantDataByRoleIdUserId(roleId, userId).then( response => {
        this.particpants = response;
        if (this.events && this.particpants) {
          this.events.forEach(x => {
            let report: Report = new Report;
            report.EventID = x.eventID;
            report.EventName = x.eventName;
            report.EventDescription = x.eventDescription;
            report.EventDate = x.eventDate;
            report.Month = x.month;
            report.BaseLocation = x.baseLocation;
            report.BeneficiaryName = x.beneficiaryName;
            report.VenueAddress = x.venue;
            report.CouncilName = x.councilName;
            report.Project = 'Project';
            report.Category = 'Category';
            report.TotalVolunteers = x.totalVolenteers;
            report.TotalVolunteerHours = x.totalVolenteerHours;
            report.TotalTravelHours = x.totalTravelHours;
            report.OverallVolunteeringHours = x.overallVolenteerHours;
            report.LivesImpacted = x.livesImpacted;
            report.ActivityType = 'ActivityType';
            report.Status = x.eventStatus;
            report.BusinessUnit = 'Business Unit';
      
            report.AverageRating = 0;
            report.ParticipantList = [];
            report.NotParticipatedList = [];
            report.UnregistedPartyList = [];
            report.PocList = [];
            this.particpants.forEach(p => {
              // if (p.eventId === x.eventID && p.roleId === 4) {
              if (p.eventId === x.eventID) {
                if (p.participationStatusId === 1) {
                  report.ParticipantList.push(p);
                  report.AverageRating = p.isFeedbackSubmitted ? report.AverageRating + p.eventRating : report.AverageRating;
                  report.ParticipatedFbCount = p.isFeedbackSubmitted ? report.ParticipatedFbCount + 1 : report.ParticipatedFbCount;
                } else if (p.participationStatusId === 2) {
                  report.NotParticipatedList.push(p);
                  report.NotParticipatedFbCount = p.isFeedbackSubmitted ? report.NotParticipatedFbCount + 1 : report.NotParticipatedFbCount;
                } else if (p.participationStatusId === 3) {
                  report.UnregistedPartyList.push(p);
                  report.UnregusteresFbCount = p.isFeedbackSubmitted ? report.UnregusteresFbCount + 1 : report.UnregusteresFbCount;
                }
              }
              if (p.eventId === x.eventID && p.roleId === 3) {
                let pModel: POCModel = new POCModel;
                pModel.SNo = report.PocList.length + 1;
                pModel.POCID = p.associateId;
                pModel.POCName = p.associateName;
                pModel.POCContact = p.email;
                report.PocList.push(pModel);
              }
            })
            report.AverageRating = report.ParticipatedFbCount === 0 ? report.AverageRating : +(report.AverageRating/report.ParticipatedFbCount).toFixed(1);
            reportData.push(report);
          });
          this.reportList.next(reportData);
          this.areReportsFetched = true;
        }
      });
    });
  }

  setUserRole(RoleId: number) {
    this.userRole.next(RoleId);
  }

  get getUserRole() {
    return this.userRole.asObservable();
  }

  logoutUser() {
    this.userRole.next(4);
  }
  
  ngOnDestroy(){
    this.userRole.next(4);
  }
}
