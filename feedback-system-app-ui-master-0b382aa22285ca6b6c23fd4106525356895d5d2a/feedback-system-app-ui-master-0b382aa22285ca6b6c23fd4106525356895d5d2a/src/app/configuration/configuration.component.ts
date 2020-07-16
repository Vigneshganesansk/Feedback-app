import { Component, OnInit } from '@angular/core';
import { Service } from '../_services/app.service';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { StorageService } from '../_services/storage.service';
import { Questions } from '../_models/questions';
import { AnswersFormValue } from '../_models/AnswersFormValue';
import { UserModel } from '../_models/userModel';
import { AlertService } from '../_services/alert.service';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css'],
})
export class ConfigurationComponent implements OnInit {

  errorMessage: string;

  submittedPMO: boolean;
  selectedRows: string[];
  pmoMailId: string;

  allGuys: UserModel[];
  pmoGuys: UserModel[];
  Q_ID: number;
  isAddQuestion: boolean;
  isEditQuestion: boolean;
  tableDisplay: boolean;
  newAnswers: AnswersFormValue;
  newQuestion: Questions;

  questionsData: Questions[];
  questionForm: FormGroup;
  PmoForm: FormGroup;

  multipleAnswer: boolean;
  freeTextAnswer: boolean;
  customQuestion: boolean;
  answerArray: FormArray;

  isPMO: boolean;
  fbTypes: string[] = ['Participated','Not Participated','Unregistered'];
  selectedFbType: string;

  AssignExpanded: boolean;
  PMOExpanded: boolean;
  FeedbackExpanded: boolean;

  constructor(
    private appService: Service,
    private formBuilder: FormBuilder,
    private storageService: StorageService,
    private alertService: AlertService
  ) {
   }

  ngOnInit() {
    this.submittedPMO = false;
    this.tableDisplay = true;
    this.storageService.getQuestionsData().subscribe( response => {
      this.questionsData = response;
    });
    this.storageService.getAllGuys().subscribe( response => {
      this.pmoGuys = response.filter(x => x.roleId === 2);
    });
    this.PmoForm = this.formBuilder.group({
      PmoTextArea: ['', [Validators.required, Validators.email,Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$')]],
    });
    this.questionForm = this.formBuilder.group({
      radioAns: ['',Validators.required],
      multipleAnswer: [false,Validators.required],
      freeTextAnswer: [false,Validators.required],
      customQuestion: [false,Validators.required],
      questionTextArea: ['',Validators.required],
      answerArray: this.formBuilder.array([this.createItem()]),
    });
    this.appService.configMode.subscribe(data => {
      if (data === "isPMOEdit") {
        this.isPMO = true;
      } else{
        this.isPMO = false;
      }
    });
    this.GetAllGuys();

    this.AssignExpanded = true;
    this.PMOExpanded = true;
    this.FeedbackExpanded = true;
  }
  selectionChangedHandler(){
    this.PmoForm.get('PmoTextArea').setValue(this.selectedRows[0]);
  }
  GetAllGuys(){
    this.storageService.getAllGuys().subscribe(data => {
      this.allGuys = data;
    });
  }
  SubmitPMO(){
    this.submittedPMO = true;
    let newGuy: UserModel = this.allGuys.filter(x => x.email == this.PmoForm.value['PmoTextArea'])[0];
    if(newGuy == null)
    {
      this.alertService.errorAlert("Oops!! Invalid user");
      return;
    }
    else{
      if(this.pmoGuys.map(x=>x.email).indexOf(newGuy.email) >= 0)
      {
        this.alertService.errorAlert("Oops!! Already a PMO");
        return;
      }
    }
    this.pmoGuys.push(newGuy);
    this.alertService.successAlert("Added Successfully");
  }

  RemovePMO(){
    let index = this.pmoGuys.map(x => x.email).indexOf(this.PmoForm.value['PmoTextArea']);
    if(index < 0)
    {
      this.alertService.errorAlert("Wrong PMO Data");
      return;
    }
    this.pmoGuys.splice(index, 1);
    this.alertService.successAlert("Removed successfully");
  }


  get answerArrays(): FormArray {
    return <FormArray>this.questionForm.controls.answerArray;
  }
  NewQuestion(){
    this.tableDisplay = false;
    this.isAddQuestion = true;
    this.questionForm.reset();
    this.clearFormArray(this.answerArray);
    // this.CancelForm();
  }

  ReturnToTable(){
    this.tableDisplay = true;
    this.isAddQuestion = false;
    this.isEditQuestion = false;
  }

  EditQuestion(Q_ID: number){
    this.Q_ID = Q_ID;
    let answers: AnswersFormValue = this.questionsData.find(x=>x.q_ID == Q_ID).answers;
    this.tableDisplay = false;
    this.isEditQuestion = true;


    const answerFA = this.questionForm.get('answerArray') as FormArray;
    while (answerFA.length) {
      answerFA.removeAt(0);
    }
    this.questionForm.patchValue(answers);
    answers.answerArray.forEach(x=>{
      answerFA.push(this.formBuilder.group(x))
    });
  }

  createItem() {
    return this.formBuilder.group({
      answerTextArea: [''],
    })
  }

  AddAnswer(){
    this.answerArray = this.questionForm.get('answerArray') as FormArray;
    this.answerArray.push(this.createItem());  
  }

  RemoveAnswer(index: number){
    this.answerArrays.removeAt(index);
  }
  clearFormArray = (formArray: FormArray) => {
    this.questionForm.setControl('answerArray', this.formBuilder.array([]));
  }
  CancelForm(){
    this.questionForm.reset();
    this.clearFormArray(this.answerArray);
    this.tableDisplay = true;
    this.isAddQuestion = false;
    this.isEditQuestion = false;
    return;
  }
  DeleteForm(){
    if(this.isAddQuestion)
    {
      this.alertService.errorAlert("Cannot delete");
      return;
    }
    this.questionForm.reset();
    this.clearFormArray(this.answerArray);
    this.storageService.deleteQuestions(this.Q_ID).subscribe(response => {
      if(response != null)
      {
        this.questionsData = response;
        this.alertService.successAlert("Deleted successfully");
        this.tableDisplay = true;
        this.isAddQuestion = false;
        this.isEditQuestion = false;
        return;
      }
      this.alertService.errorAlert("Sorry..There was an error :(");
    });
  }

  Submit(){
    // if(this.questionForm.invalid)
    // {
    //   this.alertService.errorAlert("Please fill out all data..");
    //   return;
    // }
    if(this.isEditQuestion)
    {
      var item = this.questionsData.find(x=>x.q_ID == this.Q_ID);
      var index = this.questionsData.indexOf(item);
      this.questionsData[index].answers = this.questionForm.value;
      this.storageService.updateQuestions(this.questionsData[index],this.Q_ID).subscribe(response => {
        this.questionsData = response;
        if(response != null)
        {
          this.alertService.successAlert("Question edited successfully!");
          this.ReturnToTable();
          this.CancelForm();
          return;
        }
        this.alertService.errorAlert("Sorry..There was an error :(");
      });

    }
    if(this.isAddQuestion)
    {
      this.newAnswers = {...this.newAnswers,...this.questionForm.value};

      this.storageService.postQuestions(this.newAnswers).subscribe(response => {
        this.questionsData = response;
        if(response != null)
        {
          this.alertService.successAlert("Question added successfully!");
          this.ReturnToTable();
          this.CancelForm();
          return;
        }
        this.alertService.errorAlert("Sorry..There was an error :(");
      });
    }
  }

  ToggleCard(cardName: string, action: number) {
    if (cardName === 'feedback') {
        this.FeedbackExpanded = action === 1 ? true : false;
    } else
    if (cardName === 'assign') {
        this.AssignExpanded = action === 1 ? true : false;
    } else
    if (cardName === 'pmo') {
        this.PMOExpanded = action === 1 ? true : false;
    }
  }



}
