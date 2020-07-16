import { Injectable, OnDestroy } from '@angular/core';
import { UserModel } from '../_models/userModel';
import { HttpClient } from '@angular/common/http';
import { StorageService } from './storage.service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map, mergeMap } from 'rxjs/operators';
import { AlertService } from './alert.service';
import { Router } from '@angular/router';




@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements OnDestroy {
  private currentUserSubject: BehaviorSubject<UserModel>;
  public currentUser: Observable<UserModel>;
  private baseUrl = 'http://localhost:54414/api/user';

  private loggedIn = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  constructor(private http: HttpClient, private storageService: StorageService, private alertService: AlertService, private router: Router) { 
    // this.currentUserSubject = new BehaviorSubject<UserModel>(JSON.parse(sessionStorage.getItem('currentUser')));    
    this.currentUserSubject = new BehaviorSubject<UserModel>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserModel {
    return this.currentUserSubject.value;
  }

  login(id: string, password: string)
  {
    // this.storageService.setUserRole(id);
    // this.loggedIn.next(true);

    return this.http.get<any>(`${this.baseUrl}/getLoggedInUserData/${id}/${password}`)
      .pipe(map((data) => {
        if (data === null) {
          throw new Error(`Invalid credentials`);
        }

        localStorage.setItem('currentUser', JSON.stringify(data));
        this.currentUserSubject.next(data);
        this.loggedIn.next(true);
        this.storageService.setUserRole(data.roleId);
        return data;
      }),
    );
  }

  LoggedOut() {
    localStorage.removeItem;
    this.loggedIn.next(false);
    this.storageService.logoutUser();
    // this.router.navigate(['/login']); // routerLink="/login" 
  }

  ngOnDestroy(){
    this.loggedIn.next(false);
  }
}
