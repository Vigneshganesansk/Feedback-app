import { Component, OnInit } from '@angular/core';
import { Service } from 'src/app/_services/app.service';
import { StorageService } from '../_services/storage.service';
import { Report } from '../_models/report';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import { Observable } from 'rxjs';
import { POCModel } from '../_models/pocModel';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css'],
  providers: [Service]

})
export class EventsComponent implements OnInit {
    reportsData: Report[];
    SelectedEvent: Report;
    ShowEventDetail: boolean;
    userRole$: Observable<number>;

    ActionExpanded: boolean;
    EventsExpanded: boolean;
    BenefExpanded: boolean;
    PartExpanded: boolean;
    NotPartExpanded: boolean;
    UnregExpanded: boolean;
    StatExpanded: boolean;
    POCExpanded: boolean;
    AnyPartFbSubmitted: boolean;
    AnyNotPartFbSubmitted: boolean;
    AnyUnregFbSubmitted: boolean;
    TotalFeedbacks: number;

    constructor(private storageService: StorageService, private authService: AuthenticationService) { }

    ngOnInit() {
        this.ShowEventDetail= false;
        // this.reportsData = this.storageService.getReportsData();
        this.userRole$ = this.storageService.getUserRole;
        this.ActionExpanded = true;
        this.EventsExpanded = true;
        this.BenefExpanded = true;
        this.PartExpanded = true;
        this.NotPartExpanded = true;
        this.UnregExpanded = true;
        this.StatExpanded = true;
        this.POCExpanded = true;

        
        if (!this.storageService.areReportsFetched) {
            let user = this.authService.currentUserValue;
            this.storageService.setEventReportData(user.roleId, user.associateId);
        }
        this.storageService.getReportData.subscribe(response => {
            this.reportsData = response;
        });
    }

    SwitchDiv(event: Report) {
        this.ShowEventDetail = !this.ShowEventDetail;
        this.SelectedEvent = event;
        this.TotalFeedbacks = event.ParticipatedFbCount + event.NotParticipatedFbCount + event.UnregusteresFbCount;
        this.AnyPartFbSubmitted = event.ParticipatedFbCount === 0;
        this.AnyNotPartFbSubmitted = event.NotParticipatedFbCount === 0;
        this.AnyUnregFbSubmitted = event.UnregusteresFbCount === 0;
    }

    ToggleCard(cardName: string, action: number) {
        if (cardName === 'actions') {
            this.ActionExpanded = action === 1 ? true : false;
        } else
        if (cardName === 'events') {
            this.EventsExpanded = action === 1 ? true : false;
        } else
        if (cardName === 'beneficiary') {
            this.BenefExpanded = action === 1 ? true : false;
        } else
        if (cardName === 'participated') {
            this.PartExpanded = action === 1 ? true : false;
        } else
        if (cardName === 'notparticipated') {
            this.NotPartExpanded = action === 1 ? true : false;
        } else
        if (cardName === 'unregister') {
            this.UnregExpanded = action === 1 ? true : false;
        } else
        if (cardName === 'statistics') {
            this.StatExpanded = action === 1 ? true : false;
        } else
        if (cardName === 'poc') {
            this.POCExpanded = action === 1 ? true : false;
        }
    }

    SendMail() {}
}
