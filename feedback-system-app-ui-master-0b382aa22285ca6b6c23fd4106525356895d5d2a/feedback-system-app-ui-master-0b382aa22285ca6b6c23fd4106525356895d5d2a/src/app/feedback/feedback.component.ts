import { Component, OnInit } from '@angular/core';
import { StorageService } from '../_services/storage.service';
import { Feedback } from '../_models/feedback';
import { AlertService } from '../_services/alert.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {
  constructor(
    private storageService : StorageService,
    private route: ActivatedRoute,
    private alertService: AlertService) { }

  EventRating: number;
  ratingErrorMsg: string;
  SubmitSuccess: boolean;

  particiapnt: any;
  selectedEvent: any;
  hasEventParticipantLoaded: boolean;

  questionsData: any[];
  hasFreeTextQuestions: boolean;
  hasMultiAnsQuestions: boolean;
  hasRatingQuestions: boolean;
  freeTextQuestions: any[] = [];
  multiAnsQuestions: any[] = [];

  feedbackUserId: number;
  feedbackEventId: string;

  ngOnInit() {
    this.EventRating = 0;
    this.hasRatingQuestions = false;
    this.hasMultiAnsQuestions = false;
    this.hasFreeTextQuestions = false;
    this.SubmitSuccess = false;
    this.hasEventParticipantLoaded = false;

    this.feedbackUserId = this.route.snapshot.queryParams['pid'];
    this.feedbackEventId = this.route.snapshot.queryParams['eid'];
    this.storageService.getParticipantById(this.feedbackUserId)
    .then(party => {
      // if (party.isFeedbackSubmitted === true && !(party.associateId === 111112 || party.associateId === 111111 || party.associateId === 111113)) {
      if (party.isFeedbackSubmitted === true) {
        this.SubmitSuccess = true;
      } else {
        this.particiapnt = party;
        this.storageService.getEventById(this.feedbackEventId).then(eventDetail => {
          this.selectedEvent = eventDetail;
          this.getQuestions();
        })
      }
    });
  }

  giveRating(rating: number) {
    this.EventRating = rating;
  }

  getQuestions() {
    this.storageService.getQuestionsData().subscribe( response => {
      this.questionsData = response.filter(x => x.answers.radioAns === this.particiapnt.participationStatusName);
      
      if (this.particiapnt.participationStatusId === 1) {
        this.hasRatingQuestions = true;
      }
      this.questionsData.forEach(x => {
          x.ErrorMsg = '';
          x.answers.selectedAnsIndex = -1;
  
          if (x.answers.freeTextAnswer) {
            this.hasFreeTextQuestions = true;
            x.answers.answerArray.push({ answerTextArea: '' });
            this.freeTextQuestions.push(x);
          } else if (x.answers.multipleAnswer) {
            this.hasMultiAnsQuestions = true;
            this.multiAnsQuestions.push(x);
          }
      });
      
      this.hasEventParticipantLoaded = true;
    });
  }

  selectedAnswer(questionItem: any, index: number) {
    questionItem.answers.selectedAnsIndex = index;
  }

  Reset() {
    this.EventRating = 0;
    this.ratingErrorMsg = '';

    this.questionsData.forEach(x => {
      x.ErrorMsg = '';
      if (x.answers.freeTextAnswer) {
        x.answers.answerArray[0].answerTextArea = '';
      } else if (x.answers.multipleAnswer) {
        x.answers.selectedAnsIndex = -1;
      }
    })
  }

  Submit() {
    if (this.validateQuestions()) {
      this.storageService.postFeedback(this.generateFeedbackList()).subscribe(
        data => {
          if (data) {
            this.alertService.successAlert("Feedback submitted successfully");
            this.storageService.updateParticipantData(this.particiapnt).subscribe(
              data => {
                if (data) {
                  this.alertService.successAlert("Participant updates successfully");
                }
                else {
                  this.alertService.errorAlert("Participant Update Failed");
                }
              }
            );
          }
          else {
            this.alertService.errorAlert("Feedback Update Failed");
          }
        }
      );
      
      this.SubmitSuccess = true;
    }
  }

  validateQuestions(): boolean {
    let hasErrors = false;
    this.ratingErrorMsg = '';

    if (this.EventRating === 0 && this.particiapnt.participationStatusId === 1) {
      this.ratingErrorMsg = '*Please rate the event.'
      hasErrors = true;
    }
    this.multiAnsQuestions.forEach(x => {
      x.ErrorMsg = '';
      if (x.answers.selectedAnsIndex === -1) {
        x.ErrorMsg = '*This is a manditory question. Please select an option.';
        hasErrors = true;
      }
    });
    this.freeTextQuestions.forEach(x => {
      x.ErrorMsg = '';
      if (x.answers.answerArray[0].answerTextArea === '') {
        x.ErrorMsg = '*This is a manditory question. Please enter an answer.';
        hasErrors = true;
      }
    });
    return !hasErrors;
  }

  generateFeedbackList(): Feedback[] {
    let fbList: Feedback[] = [];
    this.questionsData.forEach(x => {
      const fback: Feedback = new Feedback;
      fback.ParticipantId = this.feedbackUserId;
      fback.EventId = this.feedbackEventId;
      fback.Question = x.questions;
      fback.Answer = x.answers.freeTextAnswer ? x.answers.answerArray[0].answerTextArea : x.answers.answerArray[x.answers.selectedAnsIndex].answerTextArea;
      fbList.push(fback);
    });
    if (this.particiapnt.participationStatusId === 1) {
      this.particiapnt.eventRating = this.EventRating;
    }
    this.particiapnt.isFeedbackSubmitted = true;
    return fbList;
  }
}
