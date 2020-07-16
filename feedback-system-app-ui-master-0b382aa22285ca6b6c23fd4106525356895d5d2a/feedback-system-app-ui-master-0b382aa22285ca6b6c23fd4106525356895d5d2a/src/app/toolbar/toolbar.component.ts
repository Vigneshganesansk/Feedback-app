import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';
import { Service } from '../_services/app.service';
import { StorageService } from '../_services/storage.service';
import { UserModel } from '../_models/userModel';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css'],
})
export class ToolbarComponent implements OnInit {
  userRole$: Observable<number>;
  isLoggedIn$: Observable<boolean>;
  subscriptions: Subscription[];
  bool1: boolean;
  int1: number;
  user: UserModel;

  constructor(
    private authService: AuthenticationService,
    private appService: Service,
    private storageService: StorageService
  ) { }

  ngOnInit() {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.userRole$ = this.storageService.getUserRole;
    this.authService.currentUser.subscribe(data => {
      this.user = data;
    });
  }
  setPMOEdit(){
    this.appService.setConfigMode("isPMOEdit");
  }

  setQuestionEdit(){
    this.appService.setConfigMode("isQuestionEdit");
  }

  logOut() {
    this.authService.LoggedOut();
  }

}
